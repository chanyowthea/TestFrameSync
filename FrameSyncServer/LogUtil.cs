using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFrameSync
{
    public static class LogUtil
    {
        /// <summary>
        /// @Description: 获取当前堆栈的上级调用方法列表,直到最终调用者,只会返回调用的各方法,而不会返回具体的出错行数，可参考：微软真是个十足的混蛋啊！让我们跟踪Exception到行把！（不明真相群众请入） 
        /// </summary>
        /// <returns></returns>
        public static string GetStackTraceModelName()
        {
            //设置为true，这样才能捕获到文件路径名和当前行数，当前行数为GetFrames代码的函数，也可以设置其他参数
            System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(true);
            System.Diagnostics.StackFrame[] sf = st.GetFrames();
            string info = null;
            for (int i = 1; i < sf.Length; ++i)
            {
                string fileName = sf[i].GetFileName();
                if (string.IsNullOrEmpty(fileName))
                {
                    continue;
                }

                int index = fileName.LastIndexOf('\\');
                info += string.Format("{0}(at {1}:{2})\n", sf[i].GetMethod().Name,
                    fileName.Substring(index + 1), sf[i].GetFileLineNumber());
            }
            return info;
        }
    }
}
