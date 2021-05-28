using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public static class SqlDbFunctions
    {
        public static string JsonValue(string column, string path) => throw new NotSupportedException();

        public static int IsJson(string column) => throw new NotSupportedException();
    }
}