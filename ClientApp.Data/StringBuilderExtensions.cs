using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Data
{
  public  static class StringBuilderExtensions
    {
        public static void InitUpdate<T>(this StringBuilder builder,EventHandler<string> handler,string text)
        {
            builder.AppendLine(text);
            handler?.Invoke(new{},text);
        }
    }
}
