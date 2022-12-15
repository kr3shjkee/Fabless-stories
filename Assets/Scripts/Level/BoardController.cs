using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Configs;
using Cysharp.Threading.Tasks;
using Signals.Level;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Level
{
    public class BoardController : IDisposable
    {
        private readonly BoardConfig _boardConfig;
        private readonly ElementConfig _elementsConfig;
        private readonly Element.Factory _factory;
        private readonly SignalBus _signalBus;
        private readonly SoundManager _soundManager;
        

        private Element[,] _elements;
        private DiContainer _container;
        private string[] _elementKeys;
        private bool _isCanBackStep;

        private Element _firstSelected;

        private bool _isBlocked;

        public bool IsCanBackStep => _isCanBackStep;

        public BoardController(BoardConfig boardConfig, ElementConfig elementsConfig, Element.Factory factory,
            SignalBus signalBus, SoundManager soundManager)
        {
            _boardConfig = boardConfig;
            _elementsConfig = elementsConfig;
            _factory = factory;
            _signalBus = signalBus;
            _soundManager = soundManager;
        }

        public void Initialize()
        {
            GenerateElements();
            SubscribeSignals();
            _elementKeys = new string[_boardConfig.SizeX * _boardConfig.SizeY];
        }

        public void Initialize(string[] elementKeys)
        {
            GenerateElements(elementKeys);
            SubscribeSignals();
            _isCanBackStep = false;
        }

        public string[] SaveElementKeys()
        {
            string[] elementKeys = new string[_boardConfig.SizeX * _boardConfig.SizeY];
            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;
            int arrayIndex = 0;

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < column; x++)
                {
                    elementKeys[arrayIndex] = _elements[x, y].Key;
                    arrayIndex++;
                }
            }

            return elementKeys;
        }

        public void Dispose()
        {
            UnsubscribeSignals();
        }

        private void SubscribeSignals()
        {
            _signalBus.Subscribe<OnElementClickSignal>(OnElementClick);
            _signalBus.Subscribe<OnElementMatchShowSignal>(ShowElementsForMatch);
        }

        private void UnsubscribeSignals()
        {
            _signalBus.Unsubscribe<OnElementClickSignal>(OnElementClick);
            _signalBus.Unsubscribe<OnElementMatchShowSignal>(ShowElementsForMatch);
        }
        

        private void ShowElementsForMatch()
        {
            var element = SearchElementForMatch();

            if (element!=null)
            {
                element.ElementShowSelf();
            }
        }

        public async void OnBackStep()
        {
            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;
            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < column; x++)
                {
                    _elements[x, y].DestroySelf();
                }
            }

            _elements = null;
            await UniTask.Yield();
            
            GenerateElements(_elementKeys);
            _isCanBackStep = false;
        }

        private void OnElementClick(OnElementClickSignal signal)
        {
            if (_isBlocked)
                return;

            var element = signal.Element;
            if (_firstSelected == null)
            {
                _firstSelected = element;
                _firstSelected.SetSelected(true);
            }
            else
            {
                if (IsCanSwap(_firstSelected, element))
                {
                    _elementKeys = null;
                    _elementKeys = SaveElementKeys();
                    _firstSelected.SetSelected(false);
                    _firstSelected.StopShowSelf();
                    Swap(_firstSelected, element);
                    _firstSelected = null;
                    CheckBoard();
                    _signalBus.Fire<OnDoStepSignal>();
                }
                else
                {
                    if (_firstSelected == element)
                    {
                        _firstSelected.SetSelected(false);
                        _firstSelected = null;
                    }
                    else
                    {
                        _firstSelected.SetSelected(false);
                        _firstSelected = element;
                        _firstSelected.SetSelected(true);
                    }
                }
            }
        }

        private async void CheckBoard()
        {
            _isBlocked = true;

            bool isNeedRecheck;
            List<Element> elementsForCollecting = new List<Element>();

            do
            {
                isNeedRecheck = false;
                elementsForCollecting.Clear();
                elementsForCollecting = SearchLines();

                if (elementsForCollecting.Count > 0)
                {
                    _soundManager.MatchSoundPlay();
                    await DisableElements(elementsForCollecting);
                    _signalBus.Fire(new OnBoardMatchSignal(elementsForCollecting[0].Key, elementsForCollecting.Count));
                    await NormalizeBoard();
                    isNeedRecheck = true;
                }
            } while (isNeedRecheck);

            _isBlocked = false;
        }

        private List<Element> SearchLines()
        {
            List<Element> elementsForCollecting = new List<Element>();

            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < column; x++)
                {
                    if (_elements[x, y].IsActive && !elementsForCollecting.Contains(_elements[x, y]))
                    {
                        List<Element> checkResult;
                        bool needAddFirst = false;
                        checkResult = CheckHorizontal(x, y);
                        if (checkResult != null && checkResult.Count >= 2)
                        {
                            needAddFirst = true;
                            elementsForCollecting.AddRange(checkResult);
                        }

                        checkResult = CheckVertical(x, y);
                        if (checkResult != null && checkResult.Count >= 2)
                        {
                            needAddFirst = true;
                            elementsForCollecting.AddRange(checkResult);
                        }

                        if (needAddFirst)
                        {
                            elementsForCollecting.Add(_elements[x, y]);
                        }
                    }
                }
            }

            return elementsForCollecting;
        }

        private List<Element> CheckHorizontal(int x, int y)
        {
            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;

            int nextColumn = x + 1;
            int nextRow = y;

            if (nextColumn >= column)
                return null;

            List<Element> elementsInLine = new List<Element>();
            var element = _elements[x, y];

            while (_elements[nextColumn, nextRow].IsActive &&
                   element.ConfigItem.Key == _elements[nextColumn, nextRow].ConfigItem.Key)
            {
                elementsInLine.Add(_elements[nextColumn, nextRow]);
                if (nextColumn + 1 < column)
                {
                    nextColumn++;
                }
                else
                {
                    break;
                }
            }

            return elementsInLine;
        }

        private List<Element> CheckVertical(int x, int y)
        {
            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;

            int nextColumn = x;
            int nextRow = y + 1;

            if (nextRow >= row)
                return null;

            List<Element> elementsInLine = new List<Element>();
            var element = _elements[x, y];

            while (_elements[nextColumn, nextRow].IsActive &&
                   element.ConfigItem.Key == _elements[nextColumn, nextRow].ConfigItem.Key)
            {
                elementsInLine.Add(_elements[nextColumn, nextRow]);
                if (nextRow + 1 < row)
                {
                    nextRow++;
                }
                else
                {
                    break;
                }
            }

            return elementsInLine;
        }

        private async UniTask DisableElements(List<Element> elementsForCollecting)
        {
            var tasks = new List<UniTask>();
            foreach (var element in elementsForCollecting)
            {
                tasks.Add(element.Disable());
            }

            await UniTask.WhenAll(tasks);
        }

        private async UniTask NormalizeBoard()
        {
            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;
            for (int x = column - 1; x >= 0; x--)
            {
                List<Element> freeElements = new List<Element>();
                for (int y = row - 1; y >= 0; y--)
                {
                    while (y >= 0 && !_elements[x, y].IsActive)
                    {
                        freeElements.Add(_elements[x, y]);
                        y--;
                    }

                    if (y >= 0 && freeElements.Count > 0)
                    {
                        Swap(_elements[x, y], freeElements[0]);
                        freeElements.Add(freeElements[0]);
                        freeElements.RemoveAt(0);
                    }
                }
            }

            var tasks = new List<UniTask>();
            for (int y = row - 1; y >= 0; y--)
            {
                for (int x = column - 1; x >= 0; x--)
                {
                    if (!_elements[x, y].IsActive)
                    {
                        GenerateRandomElement(_elements[x, y], column, row);
                        tasks.Add(_elements[x, y].Enable());
                    }
                }
            }

            await UniTask.WhenAll(tasks);
        }

        private void GenerateRandomElement(Element element, int column, int row)
        {
            Vector2 gridPosition = element.GridPosition;
            var elements = GetPossibleElement((int)gridPosition.x, (int)gridPosition.y, column, row);
            element.SetConfig(elements);
        }

        private bool IsCanSwap(Element first, Element second)
        {
            var pos1 = first.GridPosition;
            var pos2 = second.GridPosition;

            Vector2 comparePosition = pos1;
            comparePosition.x += 1;
            if (comparePosition == pos2)
            {
                return true;
            }

            comparePosition = pos1;
            comparePosition.x -= 1;
            if (comparePosition == pos2)
            {
                return true;
            }

            comparePosition = pos1;
            comparePosition.y += 1;
            if (comparePosition == pos2)
            {
                return true;
            }

            comparePosition = pos1;
            comparePosition.y -= 1;
            if (comparePosition == pos2)
            {
                return true;
            }

            return false;
        }

        private void Swap(Element first, Element second)
        {
            
            _elements[(int)first.GridPosition.x, (int)first.GridPosition.y] = second;
            _elements[(int)second.GridPosition.x, (int)second.GridPosition.y] = first;

            Vector2 position = second.transform.localPosition;
            Vector2 gridPosition = second.GridPosition;

            second.SetLocalPosition(first.transform.localPosition, first.GridPosition);
            first.SetLocalPosition(position, gridPosition);
            _isCanBackStep = true;
        }

        private void GenerateElements()
        {
            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;
            var elementsOffset = _boardConfig.ElementOffset;
            _elements = new Element[column, row];

            var startPosition = new Vector2(-elementsOffset * column * 0.5f + elementsOffset * 0.5f,
                elementsOffset * row * 0.5f - elementsOffset * 0.5f);

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < column; x++)
                {
                    var position = startPosition + new Vector2(elementsOffset * x, -elementsOffset * y);
                    var element = _factory.Create(GetPossibleElement(x, y, column, row),
                        new ElementPosition(position, new Vector2(x, y)));
                    element.Initialize();
                    _elements[x, y] = element;
                }
            }
        }

        private void GenerateElements(string[] elementKeys)
        {
            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;
            var elementsOffset = _boardConfig.ElementOffset;
            _elements = new Element[column, row];
            int arrayIndex = 0;

            var startPosition = new Vector2(-elementsOffset * column * 0.5f + elementsOffset * 0.5f,
                elementsOffset * row * 0.5f - elementsOffset * 0.5f);

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < column; x++)
                {
                    var position = startPosition + new Vector2(elementsOffset * x, -elementsOffset * y);
                    var element = _factory.Create(_elementsConfig.GetByKey(elementKeys[arrayIndex]),
                        new ElementPosition(position, new Vector2(x, y)));
                    element.Initialize();
                    _elements[x, y] = element;
                    arrayIndex++;
                }
            }
        }

        private ElementConfigItem GetPossibleElement(int column, int row, int columnCount, int rowCount)
        {
            var tempList = new List<ElementConfigItem>(_elementsConfig.Items);

            int x = column;
            int y = row - 1;

            if (x >= 0 && x < columnCount && y >= 0 && y < rowCount)
            {
                if (_elements[x, y].IsInitialized)
                {
                    tempList.Remove(_elements[x, y].ConfigItem);
                }
            }

            x = column - 1;
            y = row;

            if (x >= 0 && x < columnCount && y >= 0 && y < rowCount)
            {
                if (_elements[x, y].IsInitialized)
                {
                    tempList.Remove(_elements[x, y].ConfigItem);
                }
            }

            return tempList[Random.Range(0, tempList.Count)];
        }

        private Element SearchElementForMatch()
        {
            var column = _boardConfig.SizeX;
            var row = _boardConfig.SizeY;
            Element elementForShow = null;

            for (int y = 0; y < row; y++)
            {
                for (int x = 0; x < column; x++)
                {
                    if (x + 1<column && _elements[x + 1, y].Key == _elements[x, y].Key)
                    {
                        if ((x + 2<column && y + 1<row) && _elements[x + 2, y + 1].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x + 2, y + 1];
                            return elementForShow;
                        }
                        else if (y-1 >= 0 && x+2<column && _elements[x + 2, y - 1].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x + 2, y - 1];
                            return elementForShow;
                        }
                        else if (x + 3<column && _elements[x + 3, y].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x+3, y];
                            return elementForShow;
                        }
                    }
                    else if (y + 1<row && _elements[x, y + 1].Key == _elements[x, y].Key)
                    {
                        if (x + 1<column && y + 2<row && _elements[x + 1, y + 2].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x + 1, y + 2];
                            return elementForShow;
                        }
                        else if (x - 1>=0 && y + 2<row && _elements[x - 1, y + 2].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x - 1, y + 2];
                            return elementForShow;
                        }
                        else if (y + 3<row && _elements[x, y + 3].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x, y+3];
                            return elementForShow;
                        }
                    }
                    else if (x + 2<column && _elements[x + 2, y].Key == _elements[x, y].Key)
                    {
                        if (x + 1<column && y + 1<row && _elements[x + 1, y + 1].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x + 1, y + 1];
                            return elementForShow;
                        }
                        else if (x + 1<column && y - 1>=0 && _elements[x + 1, y - 1].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x + 1, y - 1];
                            return elementForShow;
                        }
                        else if (x + 3<column && _elements[x + 3, y].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x, y];
                            return elementForShow;
                        }
                    }
                    else if (y + 2<row && _elements[x, y + 2].Key == _elements[x, y].Key)
                    {
                        if (x + 1<column && y + 1<row && _elements[x + 1, y + 1].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x + 1, y + 1];
                            return elementForShow;
                        }
                        else if (x + 1<column && y - 1>=0 && _elements[x + 1, y - 1].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x + 1, y - 1];
                            return elementForShow;
                        }
                        else if (y + 3<row && _elements[x, y + 3].Key == _elements[x, y].Key)
                        {
                            elementForShow = _elements[x, y];
                            return elementForShow;
                        }
                    }
                }
            }
            return elementForShow;
        }
    }
}