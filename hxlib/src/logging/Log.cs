// @author Zereao
// @date 2021-03-10 21:45
// @Steam https://steamcommunity/id/hexaiolun/

using System;

namespace HXLib.logging
{
    public class Log
    {
        private readonly string _modName;

        private Log(string modName)
        {
            _modName = modName;
        }

        public static Log GetLogger(string modName)
        {
            return new Log(modName);
        }

        public void Info(string msg)
        {
            Debug.LogFormat($"=={_modName}==：{msg}");
        }

        public void InfoFormat(string msg, params object[] args)
        {
            Debug.LogFormat($"=={_modName}==：{msg}", args);
        }

        public void Error(string msg)
        {
            Debug.LogError($"=={_modName}==：{msg}");
        }

        public void ErrorFormat(string msg, params object[] args)
        {
            Debug.LogErrorFormat($"=={_modName}==：{msg}", args);
        }

        public void Error(Exception e)
        {
            Debug.LogException(e);
        }
    }
}