﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


#if NOUNITY
namespace DlkGames.Unity.TestHelpers.TestableUnity
#else
namespace UnityEngine
#endif
{
    public class MonoBehaviour : Component
    {
        private bool _useGUILayout = true;
        public bool useGUILayout
        {
            get
            {
                AssertNull();
                return _useGUILayout;
            }
            set
            {
                AssertNull();
                _useGUILayout = value;
            }
        }

        private Action _awake;
        private Action _start;
        private Action _update;
        private Action _lateUpdate;
        private Action _fixedUpdate;
        private Action _onDisable;
        private Action _onDestroy;
        private Action _onEnable;
        private Action _onGUI;
        private Action<int> _onLevelWasLoaded;

        public void CancelInvoke(string methodName)
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public void CancelInvoke()
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public void Invoke(string methodName, float time)
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public void InvokeRepeating(string methodName, float time, float repeatRate)
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public bool IsInvoking()
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public bool IsInvoking(string methodName)
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public Coroutine StartCoroutine(string methodName)
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public Coroutine StartCoroutine(string methodName, object value)
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public Coroutine StartCoroutine(IEnumerator routine)
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public Coroutine StartCoroutine_Auto(IEnumerator routine)
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public void StopAllCoroutines()
        {
            AssertNull();
            throw new NotImplementedException();
        }
        public void StopCoroutine(string methodName)
        {
            AssertNull();
            throw new NotImplementedException();
        }

        protected override void CStart()
        {
            if (_start != null)
                _start();
        }

        protected override void CUpdate()
        {
            if (_update != null)
                _update();
        }

        protected override void CLateUpdate()
        {
            if (_lateUpdate != null)
                _lateUpdate();
        }

        protected override void CAwake()
        {
            //Get all the methods that monobehaviour can 'override'
            var methods = GetMethods(
                this,
                new List<string>()
                    {
                        "Awake",
                        "Start", 
                        "Update",
                        "LateUpdate",
                    },
                new List<Type>()
                    {
                        typeof(Action),
                        typeof(Action),
                        typeof(Action),
                        typeof(Action),
                    });

            _awake = methods[0] as Action;
            _start = methods[1] as Action;
            _update = methods[2] as Action;
            _lateUpdate = methods[3] as Action;

            if (_awake != null)
                _awake();
        }

        private object[] GetMethods(object target, List<string> methodNames, List<Type> methodTypes)
        {
            if (methodNames.Count != methodTypes.Count)
                throw new ArgumentOutOfRangeException("methodTypes", "methodNames and methodTypes must be the same length");

            MethodInfo[] methods = target.GetType().GetMethods(
                BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.FlattenHierarchy);

            object[] retMethods = new object[methodNames.Count];

            foreach (var method in methods)
            {
                var ind = methodNames.IndexOf(method.Name);
                if (ind != -1)
                {
                    retMethods[ind] = Delegate.CreateDelegate(methodTypes[ind], target, method, false);
                }
            }

            return retMethods;
        }

        public static void print(object message)
        {
            Debug.Log(message);
        }
    }
}
