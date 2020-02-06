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
        List<PlanReminderObj> _ToDoList = new List<PlanReminderObj>();
        DataStorageFile _DataStorageFile = new DataStorageFile();
        public DataStorage() {
        }
        public void SetPlanReminder(PlanReminderObj planObj) {
            _ToDoList.Add(planObj);
            try {
                _ToDoList.Sort(CompareDates);
            }
            catch {
                return;
            }
        }
        public void SetPlanReminderAt(PlanReminderObj planObj, Int32 num) {
            _ToDoList[num] = planObj;
            try {
                _ToDoList.Sort(CompareDates);
            }
            catch {
                return;
            }
        }
        public PlanReminderObj GetPlanReminder(Int32 num) {
            return _ToDoList[num];
        }
        public Int32 GetPlanRemindersNum() {
            return _ToDoList.Count;
        }
        public void RemovePlanReminderFromList(Int32 num) {
            _ToDoList.RemoveAt(num);
        }
        private static int CompareDates(PlanReminderObj first, PlanReminderObj second) {
            return first._DateTime.CompareTo(second._DateTime);
        }
        private void ParseFileToList() {
            int state = _DataStorageFile.OpenFile();
            if (state.Equals(-1)) {
                return; // file empty or not exists
            }
            else {
                string tStr;
                while ((tStr = _DataStorageFile.GetTheNextStringFromFile()) != null) {
                    PlanReminderObj planReminderObj = new PlanReminderObj();
                    try {
                        DateTime tDate = DateTime.Parse(tStr);
                        planReminderObj._DateTime = tDate;
                        tStr = _DataStorageFile.GetTheNextStringFromFile();
                        planReminderObj._Text = tStr;
                        SetPlanReminder(planReminderObj);
                    }
                    catch {
                        return; // something wrong with data in file
                    }
                }
            }
        }
        public void ReadFile() {
            ParseFileToList();
        }
        public void SaveToDoListToFile() {
            Int32 eventsNum = _ToDoList.Count;
            PlanReminderObj planReminderObj;
            for (Int32 i = 0; i < eventsNum; i++){
                planReminderObj = GetPlanReminder(i);
                _DataStorageFile.WriteLineToFile(planReminderObj._DateTime.ToString());
                _DataStorageFile.WriteLineToFile(planReminderObj._Text);
            }
        }
    }
}
