using System;
using System.Collections.Generic;



namespace GameEngine.Event
{

    /// <summary>
    /// [매니저] 이벤트를 관리하는 매니저입니다. 
    /// <br> 역할 ::</br>
    /// <br> 각 스크립트간의 종속적인(의존도가 높은) 상황을 최대한 피하기위한 대안으로 만들어진 기능입니다. </br>
    /// <br> 종속적인 스크립트일수록 기능 재활용의 한계가 명확하기 때문에 피하는것이 권장됩니다. </br>
    /// <br> 그 대안적 방법으로 중간다리 역할을 해주는 이벤트 매니저를 두어서, 이벤트 형식으로 스크립트를 실행시키면 </br>
    /// <br> 상호간의 유기성을 유지하고 각 스크립트간의 독립적 구조를 헤치지 않는 역할이 가능해집니다. </br>
    /// <br> ---------------------------------------</br>
    /// <br> 기능 ::</br>
    /// <br> 이벤트 등록 - 어디에서든 사용 할 수 있도록 함수를 이벤트 매니저에 등록시킵니다. </br>
    /// <br> 이벤트 호출 - 이벤트 매니저에 접근하여 등록된 이벤트 함수를 사용합니다. </br>
    /// </summary>
    public class Manager_Event
    {
        /// <summary>
        /// [매니저] 이벤트 매니저의 기능을 가지고 있는 모듈입니다. 
        /// </summary>
        public class Logic_EventManager
        {
            /// <summary>
            /// [데이터] 모든 이벤트가 담겨있는 이벤트 테이블입니다. 
            /// </summary>
            protected Dictionary<int, EventList> _eventTable = null;

            /// <summary>
            /// [기능] 담겨있는 모든 이벤트를 지웁니다. 
            /// </summary>
            public void ClearEvent()
            {
                if (null != _eventTable)
                {
                    _eventTable.Clear();
                }
            }

            /// <summary>
            /// [검사] 이벤트 테이블에 이벤트가 존재하는지를 확인합니다. 
            /// </summary>
            /// <returns>true : 비어있음<br>false : 이벤트가 존재함</br></returns>
            private bool IsEventTableNull()
            {

                if (null == _eventTable)
                {
                    return true;
                }

                return false;
            }

            /// <summary>
            /// [기능] 이벤트 타입에 알맞는 이벤트를 실행시킵니다.
            /// </summary>
            /// <param name="eventType"></param>
            public void CallEvent(eEventType eventType)
            {
                if (IsEventTableNull()) return;


                int parseType = (int)eventType;

                if (_eventTable.ContainsKey(parseType))
                {
                    List<Delegate> eventList = _eventTable[parseType].GetEventList();

                    foreach (EventFunc func in eventList)
                    {
                        if (func.Target is UnityEngine.Object && func.Target.Equals(null)) continue;
                        func();
                    }
                }

            }

            /// <summary>
            /// [기능] 이벤트 타입에 알맞는 이벤트를 실행시킵니다.
            /// </summary>
            public void CallEvent<T>(eEventType eventType, T param)
            {
                if (IsEventTableNull()) return;

                int parseType = (int)eventType;

                if (_eventTable.ContainsKey(parseType))
                {
                    List<Delegate> eventList = _eventTable[parseType].GetEventList();

                    foreach (EventFunc<T> func in eventList)
                    {
                        if (func.Target is UnityEngine.Object && func.Target.Equals(null)) continue;
                        func(param);
                    }
                }

            }

            /// <summary>
            /// [기능] 이벤트 타입에 알맞는 이벤트를 실행시킵니다.
            /// </summary>
            public void CallEvent<T1, T2>(eEventType eventType, T1 param1, T2 param2)
            {
                if (IsEventTableNull()) return;

                int parseType = (int)eventType;

                if (_eventTable.ContainsKey(parseType))
                {
                    List<Delegate> eventList = _eventTable[parseType].GetEventList();

                    foreach (EventFunc<T1, T2> func in eventList)
                    {
                        if (func.Target is UnityEngine.Object && func.Target.Equals(null)) continue;
                        func(param1, param2);
                    }
                }

            }

            /// <summary>
            /// [기능] 이벤트 타입에 알맞는 이벤트를 실행시킵니다.
            /// </summary>
            public void CallEvent<T1, T2, T3>(eEventType eventType, T1 param1, T2 param2, T3 param3)
            {
                if (IsEventTableNull()) return;

                int parseType = (int)eventType;

                if (_eventTable.ContainsKey(parseType))
                {
                    List<Delegate> eventList = _eventTable[parseType].GetEventList();

                    foreach (EventFunc<T1, T2, T3> func in eventList)
                    {
                        if (func.Target is UnityEngine.Object && func.Target.Equals(null)) continue;
                        func(param1, param2, param3);
                    }
                }

            }

            /// <summary>
            /// [기능] 이벤트 테이블에 이벤트를 추가합니다. 
            /// </summary>
            public void RegisterEvent(eEventType eventType, EventFunc func)
            {
                if (null == _eventTable)
                {
                    _eventTable = new Dictionary<int, EventList>();
                }

                int parseType = (int)eventType;

                if (false == _eventTable.ContainsKey(parseType))
                {
                    EventList list = new EventList();
                    list.AddFUnction(func);

                    _eventTable.Add(parseType, list);
                }
                else
                {
                    _eventTable[parseType].AddFUnction(func);
                }
            }

            /// <summary>
            /// [기능] 이벤트 테이블에 이벤트를 추가합니다. 
            /// </summary>
            public void RegisterEvent<T>(eEventType eventType, EventFunc<T> func)
            {
                if (null == _eventTable)
                {
                    _eventTable = new Dictionary<int, EventList>();
                }

                int parseType = (int)eventType;

                if (false == _eventTable.ContainsKey(parseType))
                {
                    EventList list = new EventList();
                    list.AddFUnction(func);

                    _eventTable.Add(parseType, list);
                }
                else
                {
                    _eventTable[parseType].AddFUnction(func);
                }
            }

            /// <summary>
            /// [기능] 이벤트 테이블에 이벤트를 추가합니다. 
            /// </summary>
            public void RegisterEvent<T1, T2>(eEventType eventType, EventFunc<T1, T2> func)
            {
                if (null == _eventTable)
                {
                    _eventTable = new Dictionary<int, EventList>();
                }

                int parseType = (int)eventType;

                if (false == _eventTable.ContainsKey(parseType))
                {
                    EventList list = new EventList();
                    list.AddFUnction(func);

                    _eventTable.Add(parseType, list);
                }
                else
                {
                    _eventTable[parseType].AddFUnction(func);
                }
            }

            /// <summary>
            /// [기능] 이벤트 테이블에 이벤트를 추가합니다. 
            /// </summary>
            public void RegisterEvent<T1, T2, T3>(eEventType eventType, EventFunc<T1, T2, T3> func)
            {
                if (null == _eventTable)
                {
                    _eventTable = new Dictionary<int, EventList>();
                }

                int parseType = (int)eventType;

                if (false == _eventTable.ContainsKey(parseType))
                {
                    EventList list = new EventList();
                    list.AddFUnction(func);

                    _eventTable.Add(parseType, list);
                }
                else
                {
                    _eventTable[parseType].AddFUnction(func);
                }
            }

            /// <summary>
            /// [기능] 이벤트 테이블에서 특정 이벤트를 제거합니다. 
            /// </summary>
            public void RemoveEvent(eEventType eventType, EventFunc func)
            {
                if (null == _eventTable) { return; }

                int parseType = (int)eventType;

                if (true == _eventTable.ContainsKey(parseType))
                {
                    _eventTable[parseType].RemoveFunction(func);
                }
            }

            /// <summary>
            /// [기능] 이벤트 테이블에서 특정 이벤트를 제거합니다. 
            /// </summary>
            public void RemoveEvent<T>(eEventType eventType, EventFunc<T> func)
            {
                if (null == _eventTable) { return; }

                int parseType = (int)eventType;

                if (true == _eventTable.ContainsKey(parseType))
                {
                    _eventTable[parseType].RemoveFunction(func);
                }
            }

            /// <summary>
            /// [기능] 이벤트 테이블에서 특정 이벤트를 제거합니다. 
            /// </summary>
            public void RemoveEvent<T1, T2>(eEventType eventType, EventFunc<T1, T2> func)
            {
                if (null == _eventTable) { return; }

                int parseType = (int)eventType;

                if (true == _eventTable.ContainsKey(parseType))
                {
                    _eventTable[parseType].RemoveFunction(func);
                }
            }

            /// <summary>
            /// [기능] 이벤트 테이블에서 특정 이벤트를 제거합니다. 
            /// </summary>
            public void RemoveEvent<T1, T2, T3>(eEventType eventType, EventFunc<T1, T2, T3> func)
            {
                if (null == _eventTable) { return; }

                int parseType = (int)eventType;

                if (true == _eventTable.ContainsKey(parseType))
                {
                    _eventTable[parseType].RemoveFunction(func);
                }
            }
        }


    }


    /// <summary>
    /// [대리자] 매개변수가 없는 이벤트 타입입니다.
    /// </summary>
    public delegate void EventFunc();
    /// <summary>
    /// [대리자] 매개변수가 한개인 이벤트 타입입니다.
    /// </summary>
    public delegate void EventFunc<T>(T param1);
    /// <summary>
    /// [대리자] 매개변수가 두개인 이벤트 타입입니다.
    /// </summary>
    public delegate void EventFunc<T1, T2>(T1 param1, T2 param2);
    /// <summary>
    /// [대리자] 매개변수가 세개인 이벤트 타입입니다.
    /// </summary>
    public delegate void EventFunc<T1, T2, T3>(T1 param1, T2 param2, T3 param3);

    /// <summary>
    /// [기능] 개별 이벤트 종류마다 구성되는 기본 로직입니다. 
    /// <br> 이벤트마다 실행시킬 함수를 추가하거나 제거하는 등, 기본 구성을 담고 있습니다. </br>
    /// </summary>
    public struct EventList
    {
        /// <summary>
        /// [기능] 이벤트에 담겨있는 함수리스트입니다.
        /// </summary>
        private List<Delegate> _EventList;

        /// <summary>
        /// [기능] 이벤트에 해당하는 함수 리스트입니다.
        /// </summary>
        /// <returns></returns>
        public List<Delegate> GetEventList() => _EventList;

        /// <summary>
        /// [기능] 이벤트리스트에 함수를 추가합니다.
        /// </summary>
        public void AddFUnction<T>(T func)
        {
            if (null == _EventList)
            {
                _EventList = new List<Delegate>();
            }

            _EventList.Add(func as Delegate);
        }

        /// <summary>
        /// [기능] 이벤트리스트에서 특정 함수를 제거합니다. 
        /// </summary>
        public void RemoveFunction<T>(T func)
        {
            if (null == _EventList)
            {
                return;
            }
            _EventList.Remove(func as Delegate);
        }

        /// <summary>
        /// [검사] 이벤트가 비어있는지를 확인합니다. 
        /// </summary>
        /// <returns>true : 비어있음<br>false : 함수가 있음</br></returns>
        public bool IsEmpty()
        {
            if (null == _EventList)
            {
                return true;
            }

            if (_EventList.Count <= 0) { return true; }

            return false;
        }
    }
}