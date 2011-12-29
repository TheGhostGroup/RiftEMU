/*
 * Copyright (C) 2011 Strawberry-Pr0jcts <http://strawberry-pr0jcts.com>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 */
 
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using FrameWork;

namespace FrameWork.Config
{
    public class Config
    {
        public Config(string config)
        {
            if (!File.Exists(config))
            {
                Log.Error("FileNotFound", "Configuration file " + config + " doesn't exist." );
                Thread.Sleep(5000);
                Process.GetCurrentProcess().Kill();
            }
        }

        #region Attributes
        // Config filename
        public static string ConfigFile { get; set; }

        // Readers
        FileStream configFile;
        StreamReader readConfig;

        string configOption;

        // Config name / value separator
        string[] ConfigSeperator = { "=" };

        // Value from the config option
        string[] result;
        #endregion

        public object ReadValue(string name, object defaultValue)
        {
            object nameValue = null;
            int lineCounter = 0;

            using (configFile = new FileStream(ConfigFile, FileMode.Open, FileAccess.Read))
            {
                using (readConfig = new StreamReader(configFile))
                {
                    while (!readConfig.EndOfStream)
                    {
                        configOption = readConfig.ReadLine();
                        ++lineCounter;

                        // Set result to the value on the right side of the config option
                        result = configOption.Split(ConfigSeperator, StringSplitOptions.None);
                        result[0] = result[0].Trim();
                        if (result[0].Equals(name, StringComparison.InvariantCulture))
                        {
                            foreach (object value in result)
                            {
                                if (!value.ToString().Trim().Equals(""))
                                    nameValue = value;
                            }

                            string trimmedValue = nameValue.ToString().Trim();

                            if (defaultValue is bool)
                            {
                                if (trimmedValue == "0")
                                    return false;
                                else if (trimmedValue == "1")
                                    return true;
                                else
                                    Log.Error("WrongConfigValue", "Error in " + ConfigFile + " on line " + lineCounter + ". Need to be a valid Boolean 0 or 1!");
                            }
                            if (defaultValue is int)
                            {
                                int res;
                                if (Int32.TryParse(trimmedValue, out res))
                                    return res;
                                else
                                    Log.Error("WrongConfigValue", "Error in " + ConfigFile + " on line " + lineCounter + ". Need to be a valid Integer!");
                            }
                            if (defaultValue is string)
                            {
                                return trimmedValue.Trim('"');
                            }
                        }
                    }
                }
            }

            return defaultValue;
        }
    }
}

