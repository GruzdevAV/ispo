using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorClass
{
    enum ElevatorDoorsConditions { OPENED, CLOSED }
    enum ElevatorMovingConditions { WAIT, UP, DOWN }
    public class FloorItem
    {
        public int Number { internal set; get; }
        public bool Called { internal set; get; }
        public FloorItem(int floor) { Number = floor; }
        public override string ToString()
        {
            return $"Этаж № {Number+1}, {(Called?"Вызван":"Не вызван")}";
        }
    }
    public class Elevator
    {
        int _curFloor = 0;
        ElevatorDoorsConditions _doorsConditions = ElevatorDoorsConditions.CLOSED;
        ElevatorMovingConditions _movingCondition = ElevatorMovingConditions.WAIT;
        public int TopCalledFloor
        {
            get
            {
                for (int i = Floors.Length - 1; i > -1; i--)
                {
                    if (Floors[i].Called) return i;
                }
                return -1;
            }
        }
        public int BottomCalledFloor
        {
            get
            {
                for (int i = 0; i < Floors.Length; i++)
                {
                    if (Floors[i].Called) return i;
                }
                return -1;
            }
        }
        public FloorItem[] GetFloors()
        {
            var arr = new FloorItem[Floors.Length];
            for (int i=0;i<Floors.Length;i++)
            {
                arr[i] = new FloorItem(i) ;
            }
            return arr;
        }
        private void CloseDoors()
        {
            _doorsConditions = ElevatorDoorsConditions.CLOSED;
        }
        private void OpenDoors()
        {
            _doorsConditions = ElevatorDoorsConditions.OPENED;
        }
        public bool DoorsAreOpened
        {
            get => _doorsConditions==ElevatorDoorsConditions.OPENED;
        }
        private bool ElevatorIsCalled
        {
            get
            {
                foreach (FloorItem i in Floors)
                {
                    if (i.Called) return true;
                }
                return false;
            }
        }
        private bool CurFloorIsCalled
        {
            get => Floors[_curFloor].Called;
            set => Floors[_curFloor].Called = value;
        }
        public FloorItem[] Floors { get; }

        public Elevator(int floorsNumber)
        {
            if (floorsNumber < 2) throw new Exception("Этажей должно быть как минимум 2");
            Floors = new FloorItem[floorsNumber];
            for (int i=0;i<Floors.Length;i++)
            {
                Floors[i] = new FloorItem(i);
            }
        }
        public void Move()
        {
            switch (_doorsConditions)
            {
                case ElevatorDoorsConditions.CLOSED:
                    switch (_movingCondition)
                    {
                        case ElevatorMovingConditions.WAIT:
                            if (ElevatorIsCalled)
                            {
                                if (CurFloorIsCalled)
                                {
                                    OpenDoors();
                                    CurFloorIsCalled = false;
                                }
                                else if (BottomCalledFloor < _curFloor)
                                    _movingCondition = ElevatorMovingConditions.DOWN;
                                else
                                    _movingCondition = ElevatorMovingConditions.UP;
                            }
                            break;
                        case ElevatorMovingConditions.UP: 
                            if (CurFloorIsCalled)
                            {
                                OpenDoors();
                                CurFloorIsCalled = false;
                                if (!ElevatorIsCalled)
                                    _movingCondition = ElevatorMovingConditions.WAIT;
                                else if (TopCalledFloor < _curFloor)
                                    _movingCondition= ElevatorMovingConditions.DOWN;
                                return;
                            }
                            if (_curFloor + 1 < Floors.Length)
                                _curFloor++;
                            else
                                _movingCondition = ElevatorMovingConditions.WAIT;
                            break;
                        case ElevatorMovingConditions.DOWN: 
                            if (_curFloor==BottomCalledFloor)
                            {
                                OpenDoors();
                                CurFloorIsCalled= false;
                                _movingCondition = !ElevatorIsCalled ? 
                                    ElevatorMovingConditions.WAIT : ElevatorMovingConditions.UP;
                                return;
                            }
                            if (_curFloor>0)
                                _curFloor--;
                            else
                                _movingCondition= ElevatorMovingConditions.WAIT;
                            break;
                    }
                    break;
                case ElevatorDoorsConditions.OPENED:
                    CloseDoors();
                    break;
            }
        }

        private int FindClosestFloor()
        {
            for (int i = 0; _curFloor - i > -1 || _curFloor + i < Floors.Length; i++)
            {
                if (Floors[_curFloor - i].Called) return _curFloor - i;
                if (Floors[_curFloor + i].Called) return _curFloor + i;
            }
            return -1;
        }

        public void CallElevator(int floor)
        {
            if (floor < 0 || !(floor < Floors.Length)) throw new Exception("Нельзя вызвать лифт на несуществующий этаж!");
            Floors[floor].Called = true;
        }
        public override string ToString()
        {
            return $"Этаж №{_curFloor + 1}";
        }
        public string GetCondition()
        {
            var msg = $"Этаж: {_curFloor+1}: двери {(DoorsAreOpened?"открыты":"закрыты")}, ";
            switch (_movingCondition)
            {
                case ElevatorMovingConditions.WAIT:
                    msg += "ожидание вызова.";
                    break;
                case ElevatorMovingConditions.UP:
                    msg += "лифт движется вверх.";
                    break;
                case ElevatorMovingConditions.DOWN:
                    msg += "лифт движется вниз.";
                    break;
            }
            return msg;
        }
    }
}
