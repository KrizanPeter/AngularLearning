using System;
using System.Collections.Generic;
using System.Text;

namespace BoardGame.Services.ReturnStates
{
    public class OperationalResult
    {

            public bool Succeeded { get; set; }

            public IEnumerable<OperationalError>? Errors { get; set; }

            public static OperationalResult<T> Success<T>() where T : class
            {
                return new () { Succeeded = true };
            }

            public static OperationalResult<T> Success<T>(T data) where T : class
            {
                return new(data) { Succeeded = true };
            }

            public static OperationalResult Success()
            {
                return new() { Succeeded = true };
            }

            public static OperationalResult Failed(params OperationalError[] errors)
            {
                return new() { Succeeded = false, Errors = errors };
            }

            public static OperationalResult<T> Failed<T>(params OperationalError[] errors)
                where T : class
            {
                return new() { Succeeded = false, Errors = errors };
            }
        }

        public class OperationalResult<T> : OperationalResult where T : class
        {
            public OperationalResult()
            {
            }

            public OperationalResult(T data)
            {
                Data = data;
                Succeeded = true;
            }

            public T? Data { get; }
        }
    }


