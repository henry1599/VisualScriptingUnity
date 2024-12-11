using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Baram.Utils.EditorTools
{
    public class CmdInfo 
    {
        public string Command;
    }
    public enum eRunUser
    {
        Default = 0,
        Admin = 1
    }
    public enum eCommandType
    {
        SingleAmpersand = 0,    // &    -> Run the command and then run the next command (sequentially)
        DoubleAmpersand = 1,    // &&   -> Run the command and then run the next command only if the first command succeeded
        DoublePipe = 2          // ||   -> Run the command and then run the next command only if the first command failed
    }
    public static class Cmd
    {
        public static string ToCommandOperator(this eCommandType commandType) => commandType switch
        {
            eCommandType.SingleAmpersand => "&",
            eCommandType.DoubleAmpersand => "&&",
            eCommandType.DoublePipe => "||",
            _ => "&"
        };
        public static eCommandType ToCommandType(this string commandOperator) => commandOperator switch
        {
            "&" => eCommandType.SingleAmpersand,
            "&&" => eCommandType.DoubleAmpersand,
            "||" => eCommandType.DoublePipe,
            _ => eCommandType.SingleAmpersand
        };
        private static Queue<CmdInfo> commandQueue = new Queue<CmdInfo>();

        public static CmdInfo Append(string command)
        {
            CmdInfo info = new CmdInfo()
            {
                Command = command
            };
            commandQueue.Enqueue(info);
            return info;
        }
        public static CmdInfo Append(this CmdInfo _, string command)
        {
            CmdInfo infoInstance = new CmdInfo()
            {
                Command = command
            };
            commandQueue.Enqueue(infoInstance);
            return infoInstance;
        }

        public static void Run(eRunUser user = default, eCommandType commandType = default, Action<string> onCompleted = null, Action<string> onError = null, bool printRunCommand = true)
        {
            bool isAdmin = user == eRunUser.Admin;
            string separator = string.Format(" {0} ", commandType.ToCommandOperator());
            string finalCommand = string.Join(separator, commandQueue.Select(item => item.Command));
            if (printRunCommand)
                Debug.Log(finalCommand);
            try
            {
                var processInfo = new System.Diagnostics.ProcessStartInfo("cmd.exe", "/c " + finalCommand)
                {
                    UseShellExecute = isAdmin,
                    RedirectStandardOutput = !isAdmin,
                    RedirectStandardError = !isAdmin,
                    CreateNoWindow = true,
                    Verb = isAdmin ? "runas" : ""
                };

                var process = new System.Diagnostics.Process
                {
                    StartInfo = processInfo,
                    EnableRaisingEvents = true
                };

                process.OutputDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        onCompleted?.Invoke(args.Data);
                    }
                };

                process.ErrorDataReceived += (sender, args) =>
                {
                    if (!string.IsNullOrEmpty(args.Data))
                    {
                        onError?.Invoke(process.StandardError.ReadToEnd());
                    }
                };

                process.Exited += (sender, args) =>
                {
                    process.Dispose();
                    commandQueue.Clear();
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }
            catch (Exception e)
            {
                // Debug.LogError(e.Message);
                onError?.Invoke(e.Message);
                commandQueue.Clear();
            }
        }
        public static void Run(this CmdInfo info, eRunUser user = default, eCommandType commandType = default, Action<string> onCompleted = null, Action<string> onError = null, bool printRunCommand = true)
        {
            Run(user, commandType, onCompleted, onError, printRunCommand);
        }
    }
}
