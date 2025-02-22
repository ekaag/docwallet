using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace docwallet.indexer.Logger;

/// <summary>
/// To be removed by asimov logger
/// </summary>
public class ConsoleWriter : StreamWriter
{
    TextWriter console;
    public ConsoleWriter(string path, bool append, TextWriter consoleout)
        : base(path, append)
    {
        this.console = consoleout;
        base.AutoFlush = true;
    }
    public override void Write(string value)
    {
        console.Write(value);
        //base.Write(value);//do not log writes without line ends as these are only for console display
    }
    public override void WriteLine()
    {
        console.WriteLine();
        //base.WriteLine();//do not log empty writes as these are only for advancing console display
    }
    public override void WriteLine(string value)
    {
        console.WriteLine(value);
        if (value != "")
        {
            base.WriteLine(value);
        }
    }
    public new void Dispose()
    {
        base.Dispose();
    }
}
