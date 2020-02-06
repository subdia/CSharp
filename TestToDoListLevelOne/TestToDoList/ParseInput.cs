using System;
namespace TestToDoList {
    public enum CommandType {
        Help,
        View,
        New,
        NewDataDateTime,
        NewDataEventText,
        Edit,
        EditArgNum,
        EditArgDateTime,
        EditArgEventText,
        Delete,
        DeleteArg,
        Exit,
        Trash,
    }

    public class ParseInput {
        static CommandType _PreviousCmd = CommandType.Trash;
        static CommandType _CurrentCmd = CommandType.Trash;
        static String _StaticStr;
        public ParseInput() {}
        public CommandType GetInputCommand() {
            String str = Console.ReadLine();
            _StaticStr = str;
            // sort commands first
            if (String.Equals(str, "help")) {
                _PreviousCmd = CommandType.Help;
                return CommandType.Help;
            }
            else if (String.Equals(str, "view")) {
                _PreviousCmd = CommandType.View;
                return CommandType.View;
            }
            else if (String.Equals(str, "new")){
                _PreviousCmd = CommandType.New;
                return CommandType.New;
            }
            else if (String.Equals(str, "edit")) {
                _PreviousCmd = CommandType.Edit;
                return CommandType.Edit;
            }
            else if (String.Equals(str, "delete")) {
                _PreviousCmd = CommandType.Delete;
                return CommandType.Delete;
            }
            else if (String.Equals(str, "exit")){
                return CommandType.Exit;
            }
            // try to sort commands args then
            else{
                switch(_PreviousCmd) {
                    case CommandType.New:
                        _CurrentCmd = CommandType.NewDataDateTime;
                        break;
                    case CommandType.NewDataDateTime:
                        _CurrentCmd = CommandType.NewDataEventText;
                        break;
                    case CommandType.Edit:
                        _CurrentCmd = CommandType.EditArgNum;
                        break;
                    case CommandType.EditArgNum:
                        _CurrentCmd = CommandType.EditArgDateTime;
                        break;
                    case CommandType.EditArgDateTime:
                        _CurrentCmd = CommandType.EditArgEventText;
                        break;
                    case CommandType.Delete:
                        _CurrentCmd = CommandType.DeleteArg;
                        break;
                    default:
                        _CurrentCmd = CommandType.Trash;
                        break;
                }
                _PreviousCmd = _CurrentCmd;
            }
            return _CurrentCmd;
        }

        public String GetInputString() {
            return _StaticStr;
        }
        public void ResetCmd() {
            _CurrentCmd = CommandType.Trash;
           _PreviousCmd = CommandType.Trash;
        }
    }
}
