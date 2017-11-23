using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeJs;

namespace Veggie
{
    /*
     * Your javascript code in Edge.js must return a function with signature:
     * ` function(input, callback) `
     * and call the `callback` with signature
     * ` callback(error, output) `
     * which in real life looks like this:
     * 
     * return function(input, cb) {
     *     ...
     *     cb(myError, myOutput)
     * }
     */
    public abstract class EdgeWrapper
    {
        public abstract object Options { get; }
        public abstract string JsCode { get; }

        public Func<object, Task<object>> JsFunc { get; private set; }

        public void GenerateJsFunc()
        {
            JsFunc = Edge.Func(JsCode);
        }

        public object Run()
        {
            var task = JsFunc(Options);
            task.Wait();
            return task.Result;
        }

        public Task<object> RunAsync() => JsFunc(Options);
    }

//  #pragma warning disable 1998
//    public class MyJsClass : EdgeWrapper
//    {
//        private object _cachedData;
//        public override object Options => new
//        {
//            MyDelegate = (Func<object, Task<object>>)(async inputData =>
//            {
//                _cachedData = inputData;
//                return $"Awesome {inputData} FTW!";
//            })
//        };

//        public override string JsCode => @"
//            return function(input, cb) {
//                var myData = generateMyAwesomeData();
//                input.MyDelegate(myData, function(error, result) {
//                    console.log(result);
//                });
//                cb(); // equivalent to returning void
//            }
//        ";
//    }
//  #pragma warning restore
}
