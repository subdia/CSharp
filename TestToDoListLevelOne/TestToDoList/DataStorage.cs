using System;
using System.Collections.Generic;

namespace TestToDoList {
    public struct PlanReminderObj {
        public DateTime _DateTime { get; set; }
        public String _Text { get; set; }
        public void ViewInfo() {
            Console.Write($"Date and time: {_DateTime}  ToDo: {_Text}");
        }
    }
    
    public class DataStorage {
        List<PlanReminderObj> ToDoList = new List<PlanReminderObj>();

        public DataStorage() {
        }
        public void SetPlanReminder(PlanReminderObj planObj) {
            ToDoList.Add(planObj);
            try {
                ToDoList.Sort(CompareDates);
            }
            catch {
                return;
            }
        }
        public void SetPlanReminderAt(PlanReminderObj planObj, Int32 num) {
            ToDoList[num] = planObj;
            try {
                ToDoList.Sort(CompareDates);
            }
            catch {
                return;
            }
        }
        public PlanReminderObj GetPlanReminder(Int32 num) {
            return ToDoList[num];
        }
        public Int32 GetPlanRemindersNum() {
            return ToDoList.Count;
        }
        public void RemovePlanReminderFromList(Int32 num) {
            ToDoList.RemoveAt(num);
        }
        private static int CompareDates(PlanReminderObj first, PlanReminderObj second) {
            return first._DateTime.CompareTo(second._DateTime);
        }
    }
}
