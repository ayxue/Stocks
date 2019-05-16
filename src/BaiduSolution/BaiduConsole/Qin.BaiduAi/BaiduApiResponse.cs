using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qin.BaiduAi
{
    public static class BaiduApiResponseExt
    {
        public static BaiduApiResponse<T> ToResponse<T>(this JObject respObj, bool keepRaw = false)
        {
            var response = new BaiduApiResponse<T>();
            ForError(response, respObj);
            ForResult(response, respObj);
            ForRaw(response, respObj, keepRaw);
            return response;
        }

        public static BaiduApiResponse<T> ToResponse<T>(this JObject respObj, string path, bool keepRaw = false)
        {
            var response = new BaiduApiResponse<T>();
            ForError(response, respObj);
            ForResult(response, respObj, path);
            ForRaw(response, respObj, keepRaw);
            return response;
        }

        private static void ForError<T>(BaiduApiResponse<T> response, JObject respObj)
        {
            if (respObj["error_code"] != null && (int)respObj["error_code"] > 0)
            {
                response.ErrorCode = (int)respObj["error_code"];
                response.ErrorMessage = (string)respObj["error_msg"];
            }
            else if(respObj["err_no"] != null)
            {
                int errNo;
                if(int.TryParse((string)respObj["err_no"], out errNo))
                {
                    if(errNo > 0)
                    {
                        response.ErrorCode = errNo;
                        response.ErrorMessage = (string)respObj["err_msg"];
                    }
                }
            }
        }

        private static void ForResult<T>(BaiduApiResponse<T> response, JObject respObj, string path = null)
        {
            if(string.IsNullOrEmpty(path))
                response.Result = respObj.ToObject<T>();
            else
            {
                var segments = path.Split('.');
                JToken target = GetTargetByPath(respObj, segments);

                if (target != null)
                    response.Result = target.ToObject<T>();
            }
        }

        private static JToken GetTargetByPath(JToken token, string[] segments, int index = 0)
        {
            if (segments == null || segments.Length == index)
                return token;

            if (token == null)
                return token;

            var segment = segments[index];
            if (string.IsNullOrEmpty(segment))
                return token;

            var target = token[segment];
            return GetTargetByPath(target, segments, index + 1);
        }


        private static void ForRaw<T>(BaiduApiResponse<T> response, JObject respObj, bool keepRaw)
        {
            if (keepRaw)
                response.Raw = respObj.ToString();
        }

    }

    [DebuggerDisplay("{Result}")]
    public class BaiduApiResponse<T>
    {
        public int? ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccess
        {
            get
            {
                return ErrorCode.HasValue;
            }
        }

        public string StatusCode { get; set; }

        public T Result { get; set; }

        public string Raw { get; set; }

    }
}
