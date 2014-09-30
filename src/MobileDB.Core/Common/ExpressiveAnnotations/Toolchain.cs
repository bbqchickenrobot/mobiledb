#region Copyright (C) 2014 Dennis Bappert

// The MIT License (MIT)

// Copyright (c) 2014 Dennis Bappert

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

#endregion

using System;
using System.Text.RegularExpressions;

namespace MobileDB.Common.ExpressiveAnnotations
{
    /// <summary>
    ///     Contains a set of predefined methods.
    /// </summary>
    internal static class Toolchain
    {
        /// <summary>
        ///     Registers methods for expressions.
        /// </summary>
        /// <param name="parser">Parser.</param>
        public static void RegisterMethods(this Parser parser)
        {
            parser.AddFunction("Now", () => DateTime.Now);
            parser.AddFunction("Today", () => DateTime.Today);
            parser.AddFunction<string, int>("Length", str => str != null ? str.Length : 0);
            parser.AddFunction<string, string>("Trim", str => str != null ? str.Trim() : null);
            parser.AddFunction<string, string, string>("Concat", (strA, strB) => string.Concat(strA, strB));
            parser.AddFunction<string, string, string, string>("Concat",
                (strA, strB, strC) => string.Concat(strA, strB, strC));
            parser.AddFunction<string, string, int>("CompareOrdinal",
                (strA, strB) =>
                    string.Compare(strA, strB, StringComparison.Ordinal) > 0
                        ? 1
                        : string.Compare(strA, strB, StringComparison.Ordinal) < 0 ? -1 : 0);
            parser.AddFunction<string, string, int>("CompareOrdinalIgnoreCase",
                (strA, strB) =>
                    string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) > 0
                        ? 1
                        : string.Compare(strA, strB, StringComparison.OrdinalIgnoreCase) < 0 ? -1 : 0);
            parser.AddFunction<string, string, bool>("StartsWith",
                (str, prefix) => str != null && prefix != null && str.StartsWith(prefix));
            parser.AddFunction<string, string, bool>("StartsWithIgnoreCase",
                (str, prefix) =>
                    str != null && prefix != null && str.StartsWith(prefix, StringComparison.OrdinalIgnoreCase));
            parser.AddFunction<string, string, bool>("EndsWith",
                (str, suffix) => str != null && suffix != null && str.EndsWith(suffix));
            parser.AddFunction<string, string, bool>("EndsWithIgnoreCase",
                (str, suffix) =>
                    str != null && suffix != null && str.EndsWith(suffix, StringComparison.OrdinalIgnoreCase));
            parser.AddFunction<string, string, bool>("Contains",
                (str, substr) => str != null && substr != null && str.Contains(substr));
            parser.AddFunction<string, string, bool>("ContainsIgnoreCase",
                (str, substr) => str != null && substr != null && str.ToLower().Contains(substr.ToLower()));
            parser.AddFunction<string, bool>("IsNullOrWhiteSpace", str => string.IsNullOrWhiteSpace(str));
            parser.AddFunction<string, bool>("IsDigitChain", str => Regex.IsMatch(str, @"^\d+$"));
            parser.AddFunction<string, bool>("IsNumber",
                str => Regex.IsMatch(str, @"^[\+-]?\d*\.?\d+(?:[eE][\+-]?\d+)?$"));
            parser.AddFunction<string, bool>("IsEmail",
                str =>
                    Regex.IsMatch(str,
                        @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$"));
            // taken from HTML5 specification: http://www.w3.org/TR/html5/forms.html#e-mail-state-(type=email)
            parser.AddFunction<string, bool>("IsUrl",
                str =>
                    Regex.IsMatch(str,
                        @"^(?:(?:https?|ftp):\/\/)(?:\S+(?::\S*)?@)?(?:(?!(?:10|127)(?:\.\d{1,3}){3})(?!(?:169\.254|192\.168)(?:\.\d{1,3}){2})(?!172\.(?:1[6-9]|2\d|3[0-1])(?:\.\d{1,3}){2})(?:[1-9]\d?|1\d\d|2[01]\d|22[0-3])(?:\.(?:1?\d{1,2}|2[0-4]\d|25[0-5])){2}(?:\.(?:[1-9]\d?|1\d\d|2[0-4]\d|25[0-4]))|(?:(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)(?:\.(?:[a-z\u00a1-\uffff0-9]-*)*[a-z\u00a1-\uffff0-9]+)*(?:\.(?:[a-z\u00a1-\uffff]{2,})))(?::\d{2,5})?(?:\/\S*)?$",
                        RegexOptions.IgnoreCase));
            // contributed by Diego Perini: https://gist.github.com/dperini/729294 (https://mathiasbynens.be/demo/url-regex)
            parser.AddFunction<string, string, bool>("IsRegexMatch", (str, regex) => Regex.IsMatch(str, regex));
            parser.AddFunction<string, Guid>("Guid", str => new Guid(str));
        }
    }
}