using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models.ViewModels
{
    public static class Sessions
    {
        public static string CurrentExamId = nameof(CurrentExamId);
        public static string CurrentExam = nameof(CurrentExam);
        public static string CurrentQuestionId = nameof(CurrentQuestionId);
        public static string CurrentQuestionStartTime = nameof(CurrentQuestionStartTime);
        public static string AllQuestions = nameof(AllQuestions);
        public static string AllAnswers = nameof(AllAnswers);
        public static string CurrentUserQuestions = nameof(CurrentUserQuestions);

        public static T GetObject<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
