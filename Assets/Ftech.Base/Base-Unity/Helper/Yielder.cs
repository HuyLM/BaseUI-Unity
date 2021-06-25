using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
namespace Ftech.Lib.Helper {
    public class Yielder {
        private static readonly Dictionary<float, object> TimeInterval = new Dictionary<float, object>(100);

        public static readonly WaitForEndOfFrame EndOfFrame = new WaitForEndOfFrame();

        public static readonly WaitForFixedUpdate FixedUpdate = new WaitForFixedUpdate();

        public static WaitForSeconds Wait(float seconds) {
            if (TimeInterval.ContainsKey(seconds))
                return TimeInterval[seconds] as WaitForSeconds;
            WaitForSeconds value = new WaitForSeconds(seconds);
            TimeInterval.Add(seconds, value);
            return value;
        }

        public static WaitForSeconds WaitForMiliseconds(uint miliseconds) {
            float seconds = miliseconds / 1000f;
            return Wait(seconds);
        }
        /*
        #region Extend Wait
        public class WaitForTaskCompletion : CustomYieldInstruction {
            Task task;
            Action onCompleted;
            Action<string> onFailed;

            public WaitForTaskCompletion(Task task, Action onCompleted = null, Action<string> onFailed = null) {
                this.task = task;
                this.onCompleted = onCompleted;
                this.onFailed = onFailed;
            }

            public override bool keepWaiting {
                get {
                    if (task == null)
                        return false;

                    if (task.IsCompleted) {
                        Callback.CallSchedule(onCompleted);
                        return false;
                    }
                    else if (task.IsFaulted || task.IsCanceled) {
                        Callback.CallSchedule(onFailed, task.Exception.ToString());
                        return false;
                    }

                    return true;
                }
            }
        }

        public class WaitForTaskCompletion<T> : CustomYieldInstruction {
            Task<T> task;
            Action<T> onCompleted;
            Action<string> onFailed;

            public WaitForTaskCompletion(Task<T> task, Action<T> onCompleted = null, Action<string> onFailed = null) {
                this.task = task;
                this.onCompleted = onCompleted;
                this.onFailed = onFailed;
            }

            public override bool keepWaiting {
                get {
                    if (task == null) {
                        Callback.CallSchedule(onFailed, "Task is null");
                        return false;
                    }

                    if (task.IsCompleted) {
                        try { Callback.CallSchedule(onCompleted, task.Result); }
                        catch {
                            Logs.Log($"Task<{typeof(T).Name}> is completed but no content result");
                            Callback.CallSchedule(onCompleted, default);
                        }
                        return false;
                    }
                    else if (task.IsFaulted || task.IsCanceled) {
                        Callback.CallSchedule(onFailed, task.Exception.ToString());
                        return false;
                    }

                    return true;
                }
            }
        }
        #endregion
        */
    }
}
