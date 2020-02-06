using System;
namespace TestToDoList {
    public class CommandResponse {
        WindowView _WindowView = new WindowView();
        DataStorage _DataStorage = new DataStorage();
        ParseInput _ParseInput = new ParseInput();
        PlanReminderObj _ComRespReminderObj;
        Int32 _EditEventNum = 0;

        public CommandResponse() {}
        public void Response(CommandType cmdType) {
            switch (cmdType) {
                case CommandType.Help:
                    HandleHelp();
                    break;
                case CommandType.View:
                    HandleView();
                    break;
                case CommandType.New:
                    HandleNew();
                    break;
                case CommandType.NewDataDateTime:
                    HandleNewDateTime();
                    break;
                case CommandType.NewDataEventText:
                    HandleNewEventText();
                    break;
                case CommandType.Edit:
                    HandleEdit();
                    break;
                case CommandType.EditArgNum:
                    _EditEventNum = HandleEditTakeNum();
                    break;
                case CommandType.EditArgDateTime:
                    HandleEditDateTime(_EditEventNum);
                    break;
                case CommandType.EditArgEventText:
                    HandleEditEventText(_EditEventNum);
                    break;
                case CommandType.Delete:
                    _WindowView.DeleteViewStart();
                    break;
                case CommandType.DeleteArg:
                    HandleDelete();
                    _WindowView.DeleteViewComplete();
                    break;
                
                case CommandType.Trash:
                    _WindowView.TrashView();
                    break;
                default:
                    break;
            }
        }

        private void HandleHelp() {
            _WindowView.HelpView();
        }

        private void HandleView() {
            Int32 eventsNum = _DataStorage.GetPlanRemindersNum();
            if (eventsNum == 0) {
                _WindowView.SendTextToConsole("You have no events to view");
            }
            else {
                _WindowView.SendTextToConsole("Existing events found:");
                DateTime tDateTime = DateTime.Now;
                for (Int32 i = 0; i < eventsNum; i++) {
                    PlanReminderObj reminderObj = _DataStorage.GetPlanReminder(i);
                    _WindowView.SendTextToConsoleAtSameLine((i + 1).ToString());
                    _WindowView.PasteSpaceToConsole();
                    reminderObj.ViewInfo();
                    if (reminderObj._DateTime.CompareTo(tDateTime) < 0) {
                        _WindowView.SendTextToConsoleAtSameLine(" - outdated\n");
                    }
                    else {
                        _WindowView.SendTextToConsoleAtSameLine("\n");
                    }
                }
            }
        }

        private void HandleNew() {
            _WindowView.NewViewDateTime();
            _ComRespReminderObj = new PlanReminderObj();
        }

        private void HandleNewDateTime() {
            String tString = _ParseInput.GetInputString();
            try {
                DateTime tDate = DateTime.ParseExact(tString, "yyyy-MM-dd HH:mm tt", null);
                _ComRespReminderObj._DateTime = tDate;
                _WindowView.NewViewEventText();
            }
            catch {
                _ParseInput.ResetCmd();
                _WindowView.NewDateTimeError();
            }
        }

        private void HandleNewEventText() {
            String teString = _ParseInput.GetInputString();
            _ComRespReminderObj._Text = teString;
            _DataStorage.SetPlanReminder(_ComRespReminderObj);
            _WindowView.NewViewEventComplete();
        }

        private void HandleEdit(){
            _WindowView.EditViewStart();
        }

        private Int32 HandleEditTakeNum() {
            Int32 eventsNum = _DataStorage.GetPlanRemindersNum();
            String tString = _ParseInput.GetInputString();
            Int32 fromString = 0;
            try {
                fromString = (Int32.Parse(tString) - 1);
                if (fromString > eventsNum) {
                    _WindowView.SendTextToConsole("Sorry, you have less stored " +
                        "events than you've been entered. Try again.");
                }
                else {
                    _ComRespReminderObj = _DataStorage.GetPlanReminder(fromString);
                    _WindowView.SendTextToConsole("Enter new date and time of the event:");
                    _WindowView.SendTextToConsole(_ComRespReminderObj._DateTime.ToString());
                }
            }
            catch {
                _ParseInput.ResetCmd();
                _WindowView.SendTextToConsole("Sorry, can't recognize the number.");
            }
            return fromString;
        }

        private void HandleEditDateTime(Int32 num) {
            String tString = _ParseInput.GetInputString();
            try {
                DateTime tDate = DateTime.ParseExact(tString, "yyyy-MM-dd HH:mm tt", null);
                SetStaticObjDateTime(tDate);
                _WindowView.SendTextToConsole("Enter new text of the event:");
                _WindowView.SendTextToConsole(_ComRespReminderObj._Text);
            }
            catch {
                _ParseInput.ResetCmd();
                _WindowView.NewDateTimeError();
            }
        }

        private void HandleEditEventText(Int32 num) {
            String tString = _ParseInput.GetInputString();
            SetStaticObjText(tString);
            _DataStorage.SetPlanReminderAt(_ComRespReminderObj, num);
            _WindowView.EditViewComplete();
        }

        private void HandleDelete() {
            Int32 eventsNum = _DataStorage.GetPlanRemindersNum();
            String tString = _ParseInput.GetInputString();
            Int32 fromString = 0;
            try {
                fromString = (Int32.Parse(tString) - 1);
                if (fromString > eventsNum) {
                    _WindowView.SendTextToConsole("Sorry, you have less stored " +
                    	"events than you've been entered. Try again.");
                }
                else {
                    _DataStorage.RemovePlanReminderFromList(fromString);
                }
            }
            catch {
                _ParseInput.ResetCmd();
                _WindowView.SendTextToConsole("Sorry, can't recognize the number.");
            }

        }
        private void SetStaticObjDateTime(DateTime dateTime) {
            _ComRespReminderObj._DateTime = dateTime;
        }
        private void SetStaticObjText(String text) {
            _ComRespReminderObj._Text = text;
        }
    }
}
