﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Svg;
using Xml;

namespace SvgXml
{
    internal class Program
    {
        internal class ElementInfo
        {
            public string? Name { get; set; }
            public Type? Type { get; set; }
        }

        private static void PrintAttributeUsage(Action<string> write)
        {
            var elements = typeof(SvgDocument).Assembly
                .GetExportedTypes()
                .Where(x => x.GetCustomAttributes(typeof(ElementAttribute), true).Length > 0 && x.IsSubclassOf(typeof(Element)))
                .Select(x => new ElementInfo { Name = ((ElementAttribute)x.GetCustomAttributes(typeof(ElementAttribute), true)[0]).Name, Type = x })
                .OrderBy(x => x.Name);

            foreach (var element in elements)
            {
                write($"{element.Name} [{element.Type?.Name}]");
            }
        }

        private static void Main(string[] args)
        {
            Action<string> write = Console.WriteLine;

            if (args.Length == 0)
            {
                PrintAttributeUsage(write);
                return;
            }

            if (args.Length != 1)
            {
                write($"Usage: {nameof(SvgXml)} <directory>");
                return;
            }

            var directory = new DirectoryInfo(args[0]);
            var paths = new List<FileInfo>();
            var files = Directory.EnumerateFiles(directory.FullName, "*.svg");
            if (files != null)
            {
                foreach (var path in files)
                {
                    paths.Add(new FileInfo(path));
                }
            }
            paths.Sort((f1, f2) => f1.Name.CompareTo(f2.Name));

            var sw = Stopwatch.StartNew();

            var results = new List<(FileInfo path, SvgDocument document)>();
            var elementFactory = new SvgElementFactory();
            var errors = new List<(FileInfo path, Exception ex)>();

            foreach (var path in paths)
            {
                try
                {
                    var document = SvgDocument.Open(path.FullName);
                    if (document != null)
                    {
                        results.Add((path, document));
                    }
                }
                catch (Exception ex)
                {
                    errors.Add((path, ex));
                }
            }

            sw.Stop();
            write($"# {sw.Elapsed.TotalMilliseconds}ms [{sw.Elapsed}], {paths.Count} files");
#if true
            void Print(Exception ex)
            {
                write(ex.Message);
                if (ex.StackTrace != null)
                {
                    write(ex.StackTrace);
                }
                if (ex.InnerException != null)
                {
                    Print(ex.InnerException);
                }
            }
            foreach (var error in errors)
            {
                write($"{error.path.FullName}");
                Print(error.ex);
            }
#endif
#if true
            foreach (var result in results)
            {
                write($"# {result.path.FullName}");
                var document = result.document;
                if (document != null)
                {
                    document.Print(write, printAttributes: true);
                }
            }
#endif
        }
    }
}
