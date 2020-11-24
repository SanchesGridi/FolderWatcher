using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static System.Environment;

namespace FolderWatcher.BLL.Services.Base
{
    internal abstract class ErrorHistory
    {
        private int _counter;
        private Dictionary<string, MethodBase> _errorDictionary;

        protected virtual int Counter
        {
            get { return _counter; }
            set { _counter = value; }
        }
        protected virtual Dictionary<string, MethodBase> ErrorDictionary
        {
            get { return _errorDictionary ?? (_errorDictionary = new Dictionary<string, MethodBase>()); }
            set { _errorDictionary = value; }
        }

        protected virtual void AddError(string exception, string description, string service, MethodBase target) 
        {
            ErrorDictionary.Add(string.Format("{0})Exception: {1}{2}Description: {3}{2}Service: {4}", ++Counter, exception, NewLine, description, service), target);
        }

        protected virtual IEnumerable<string> GetErrorKeysLog()
        {
            var error_strings = ErrorDictionary.Keys.ToList();
            return error_strings;
        }
        protected virtual IEnumerable<MethodBase> GetErrorValuesLog()
        {
            var error_methods = ErrorDictionary.Values.ToList();
            return error_methods;
        }
        protected virtual Dictionary<string, MethodBase> GetFullLog()
        {
            var error_dictionary = ErrorDictionary;
            return error_dictionary; 
        }
    }
}