using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

namespace MB.WinBase
{
    /// <summary>
    ///UI 消息器
    /// </summary>
    public class AppMessenger
    {
        #region Constructor

        public AppMessenger() {

        }

        #endregion //  Constructor

        #region Default Instance...
        private static Object _Obj = new object();
        private static AppMessenger _Instance;
        /// <summary>
        /// 多线程安全的单实例模式。
        /// 由于对外公布，该实现方法不使用SingletionProvider 的当时来进行。
        /// </summary>
        public static AppMessenger DefaultMessenger {
            get {
                if (_Instance == null) {
                    lock (_Obj) {
                        if (_Instance == null)
                            _Instance = new AppMessenger();
                    }
                }
                return _Instance;
            }
        }
        #endregion Default Instance...

        #region  Publish
        /// <summary>
        /// 注册一个接收消息的回调方法。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        public void Subscribe(string message, Action callback) {
            this.Subscribe(message, callback, null);
        }

        /// <summary>
        /// 注册一个接收消息的回调方法。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="callback"></param>
        public void Subscribe<T>(string message, Action<T> callback) {
            this.Subscribe(message, callback, typeof(T));
        }

        void Subscribe(string message, Delegate callback, Type parameterType) {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException("'message' cannot be null or empty.");

            if (callback == null)
                throw new ArgumentNullException("callback");

            this.VerifyParameterType(message, parameterType);

            _messageToActionsMap.AddAction(message, callback.Target, callback.Method, parameterType);
        }

        [Conditional("DEBUG")]
        void VerifyParameterType(string message, Type parameterType) {
            Type previouslyRegisteredParameterType = null;
            if (_messageToActionsMap.TryGetParameterType(message, out previouslyRegisteredParameterType)) {
                if (previouslyRegisteredParameterType != null && parameterType != null) {
                    if (!previouslyRegisteredParameterType.Equals(parameterType))
                        throw new InvalidOperationException(string.Format(
                            "The registered action's parameter type is inconsistent with the previously registered actions for message '{0}'.\nExpected: {1}\nAdding: {2}",
                            message,
                            previouslyRegisteredParameterType.FullName,
                            parameterType.FullName));
                }
                else {
                    if (previouslyRegisteredParameterType != parameterType)   
                    {
                        throw new TargetParameterCountException(string.Format(
                            "The registered action has a number of parameters inconsistent with the previously registered actions for message \"{0}\".\nExpected: {1}\nAdding: {2}",
                            message,
                            previouslyRegisteredParameterType == null ? 0 : 1,
                            parameterType == null ? 0 : 1));
                    }
                }
            }
        }

        #endregion 

        #region Publish

        /// <summary>
        /// 通知所有注册的消息.
        /// </summary>
        /// <param name="messageKey"></param>
        /// <param name="parameter"></param>
        public void Publish(string messageKey, object parameter) {
            if (String.IsNullOrEmpty(messageKey))
                throw new ArgumentException("'message' cannot be null or empty.");

            Type registeredParameterType;
            if (_messageToActionsMap.TryGetParameterType(messageKey, out registeredParameterType)) {
                if (registeredParameterType == null)
                    throw new TargetParameterCountException(string.Format("Cannot pass a parameter with message '{0}'. Registered action(s) expect no parameter.", messageKey));
            }

            var actions = _messageToActionsMap.GetActions(messageKey);
            if (actions != null)
                actions.ForEach(action => action.DynamicInvoke(parameter));
        }

        /// <summary>
        /// 通知所有注册的消息
        /// </summary>
        /// <param name="message"></param>
        public void Publish(string message) {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentException("'message' cannot be null or empty.");

            Type registeredParameterType;
            if (_messageToActionsMap.TryGetParameterType(message, out registeredParameterType)) {
                if (registeredParameterType != null)
                    throw new TargetParameterCountException(string.Format("Must pass a parameter of type {0} with this message. Registered action(s) expect it.", registeredParameterType.FullName));
            }

            var actions = _messageToActionsMap.GetActions(message);
            if (actions != null)
                actions.ForEach(action => action.DynamicInvoke());
        }

        #endregion 

        #region MessageToActionsMap [nested class]

        /// <summary>
        /// 实现消息发送
        /// </summary>
        private class MessageToActionsMap
        {
            #region Constructor

            internal MessageToActionsMap() {
            }

            #endregion // Constructor

            #region AddAction

            /// <summary>
            /// 把行为注册到集合中。
            /// </summary>
            /// <param name="message"></param>
            /// <param name="target"></param>
            /// <param name="method"></param>
            /// <param name="actionType"></param>
            internal void AddAction(string message, object target, MethodInfo method, Type actionType) {
                if (message == null)
                    throw new ArgumentNullException("message");

                if (method == null)
                    throw new ArgumentNullException("method");

                lock (_map) {
                    if (!_map.ContainsKey(message))
                        _map[message] = new List<WeakAction>();

                    _map[message].Add(new WeakAction(target, method, actionType));
                }
            }

            #endregion // AddAction

            #region GetActions

            /// <summary>
            /// 根据消息的名称获取所有行为
            /// </summary>
            /// <param name="message"></param>
            /// <returns></returns>
            internal List<Delegate> GetActions(string message) {
                if (message == null)
                    throw new ArgumentNullException("message");

                List<Delegate> actions;
                lock (_map) {
                    if (!_map.ContainsKey(message))
                        return null;

                    List<WeakAction> weakActions = _map[message];
                    actions = new List<Delegate>(weakActions.Count);
                    for (int i = weakActions.Count - 1; i > -1; --i) {
                        WeakAction weakAction = weakActions[i];
                        if (weakAction == null)
                            continue;

                        Delegate action = weakAction.CreateAction();
                        if (action != null) {
                            actions.Add(action);
                        }
                        else {
                            // The target object is dead, so get rid of the weak action.
                            weakActions.Remove(weakAction);
                        }
                    }

                    // Delete the list from the map if it is now empty.
                    if (weakActions.Count == 0)
                        _map.Remove(message);
                }

                // Reverse the list to ensure the callbacks are invoked in the order they were registered.
                actions.Reverse();

                return actions;
            }

            #endregion // GetActions

            #region TryGetParameterType

            /// <summary>
            /// 获取行为注册的参数类型 .
            /// </summary>
            /// <param name="message"></param>
            /// <param name="parameterType"></param>
            /// <returns></returns>
            internal bool TryGetParameterType(string message, out Type parameterType) {
                if (message == null)
                    throw new ArgumentNullException("message");

                parameterType = null;
                List<WeakAction> weakActions;
                lock (_map) {
                    if (!_map.TryGetValue(message, out weakActions) || weakActions.Count == 0)
                        return false;
                }
                parameterType = weakActions[0].ParameterType;
                return true;
            }

            #endregion // TryGetParameterType

            #region Fields

            //存储所有所有弱引用的Action
            readonly Dictionary<string, List<WeakAction>> _map = new Dictionary<string, List<WeakAction>>();

            #endregion // Fields
        }

        #endregion 

        #region WeakAction [nested class]

        /// <summary>
        /// 行为实现类.
        /// </summary>
        private class WeakAction
        {
            #region Constructor

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="target"></param>
            /// <param name="method"></param>
            /// <param name="parameterType"></param>
            internal WeakAction(object target, MethodInfo method, Type parameterType) {
                if (target == null) {
                    _targetRef = null;
                }
                else {
                    _targetRef = new WeakReference(target);
                }

                _method = method;

                this.ParameterType = parameterType;

                if (parameterType == null) {
                    _delegateType = typeof(Action);
                }
                else {
                    _delegateType = typeof(Action<>).MakeGenericType(parameterType);
                }
            }

            #endregion // Constructor

            #region CreateAction

            /// <summary>
            /// Delegate
            /// </summary>
            internal Delegate CreateAction() {
                if (_targetRef == null) {
                    return Delegate.CreateDelegate(_delegateType, _method);
                }
                else {
                    try {
                        object target = _targetRef.Target;
                        if (target != null)
                            return Delegate.CreateDelegate(_delegateType, target, _method);
                    }
                    catch {
                    }
                }

                return null;
            }

            #endregion 

            #region Fields

            internal readonly Type ParameterType;

            readonly Type _delegateType;
            readonly MethodInfo _method;
            readonly WeakReference _targetRef;

            #endregion
        }

        #endregion 

        #region Fields

        readonly MessageToActionsMap _messageToActionsMap = new MessageToActionsMap();

        #endregion 
    }
}
