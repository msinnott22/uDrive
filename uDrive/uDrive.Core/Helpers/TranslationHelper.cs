using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Linq;
using System.Xml;
using System;
using Umbraco.Core.Logging;

namespace uDrive.Core.Helpers
{
    public class TranslationHelper
    {
        private const string UmbracoLangPath = "~/umbraco/config/lang/";
        private const string UDriveLangPath = "~/App_Plugins/uDrive/config/lang/";

        public static IEnumerable<FileInfo> GetUmbracoLanguageFiles()
        {
            var umbPath = HostingEnvironment.MapPath(UmbracoLangPath);
            var directory = new DirectoryInfo(umbPath);
            return directory.GetFiles("*.xml");
        }

        public static IEnumerable<FileInfo> GetUDriveLanguageFiles()
        {
            var uDrivePath = HostingEnvironment.MapPath(UDriveLangPath);
            var directory = new DirectoryInfo(uDrivePath);
            return directory.GetFiles("*.xml");
        }

        public static IEnumerable<FileInfo> GetUmbracoLanguageFilesToInsertLocalisationData()
        {
            return GetUmbracoLanguageFiles().Where(x => GetUDriveLanguageFiles().Any(y => y.Name == x.Name));
        }

        public static void AddTranslations()
        {
            var uDriveFiles = GetUDriveLanguageFiles();
            LogHelper.Info<TranslationHelper>(string.Format("{0} UDrive Plugin language files to be merged into Umbraco language files.", uDriveFiles.Count()));

            var uDriveFilesArray = uDriveFiles as FileInfo[] ?? uDriveFiles.ToArray();

            var existingLangs = GetUmbracoLanguageFilesToInsertLocalisationData();
            LogHelper.Info<TranslationHelper>(string.Format("{0} Umbraco language files that match up with our UDrive language files", existingLangs.Count()));

            foreach (var lang in existingLangs)
            {
                var udrive = new XmlDocument() { PreserveWhitespace = true };
                var umb = new XmlDocument() { PreserveWhitespace = true };

                try
                {
                    var match = uDriveFilesArray.FirstOrDefault(x => x.Name == lang.Name);

                    if (match != null)
                    {
                        udrive.LoadXml(File.ReadAllText(match.FullName));
                        umb.LoadXml(File.ReadAllText(lang.FullName));

                        var areas = udrive.DocumentElement.SelectNodes("//area");

                        foreach (XmlNode area in areas)
                        {
                            var aliasToTryFind = area.Attributes["alias"];
                            var findAreaInUmbracoLang = umb.SelectSingleNode(string.Format("//area [@alias='{0}']", aliasToTryFind.Value));

                            if (findAreaInUmbracoLang == null)
                            {
                                var import = umb.ImportNode(area, true);
                                umb.DocumentElement.AppendChild(import);
                            }
                            else
                            {
                                foreach (XmlNode areaKey in area.ChildNodes)
                                {
                                    if (areaKey.NodeType == XmlNodeType.Element)
                                    {
                                        var keyAliasToTryFind = area.Attributes["alias"];
                                        var findKeyInUmbracoLang = findAreaInUmbracoLang.SelectSingleNode(string.Format("./key [@alias='{0}']", keyAliasToTryFind.Value));

                                        if (findKeyInUmbracoLang == null)
                                        {
                                            var keyImport = umb.ImportNode(areaKey, true);
                                            findAreaInUmbracoLang.AppendChild(keyImport);
                                        }
                                    }
                                }
                            }
                        }

                        umb.Save(lang.FullName);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error<TranslationHelper>("Failed to add UDrive localization values to language file", ex);
                }
            }
        }

        public static void RemoveTranslations()
        {
            var uDriveFiles = GetUDriveLanguageFiles();
            LogHelper.Info<TranslationHelper>(string.Format("{0} UDrive Plugin language files to be removed into Umbraco language files", uDriveFiles.Count()));

            var uDriveFilesArray = uDriveFiles as FileInfo[] ?? uDriveFiles.ToArray();

            var existingLangs = GetUmbracoLanguageFilesToInsertLocalisationData();
            LogHelper.Info<TranslationHelper>(string.Format("{0} Umbraco language files that match up with our UDrive language files", existingLangs.Count()));

            foreach (var lang in existingLangs)
            {
                var udrive = new XmlDocument() { PreserveWhitespace = true };
                var umb = new XmlDocument() { PreserveWhitespace = true };

                try
                {
                    var match = uDriveFilesArray.FirstOrDefault(x => x.Name == lang.Name);

                    if (match != null)
                    {
                        udrive.LoadXml(File.ReadAllText(match.FullName));
                        umb.LoadXml(File.ReadAllText(lang.FullName));

                        var areas = udrive.DocumentElement.SelectNodes("//area");

                        foreach (XmlNode area in areas)
                        {
                            var aliasToTryFind = area.Attributes["alias"];
                            var findAreaInUmbracoLang = umb.SelectSingleNode(string.Format("//area [@alias='{0}']", aliasToTryFind.Value));

                            if (findAreaInUmbracoLang != null)
                            {
                                foreach (XmlNode areaKey in area.ChildNodes)
                                {
                                    if (areaKey.NodeType == XmlNodeType.Element)
                                    {
                                        var keyAliasToTryFind = area.Attributes["alias"];
                                        var findKeyInUmbracoLang = findAreaInUmbracoLang.SelectSingleNode(string.Format("./key [@alias='{0}']", keyAliasToTryFind.Value));

                                        if (findKeyInUmbracoLang == null)
                                        {
                                            var keyToRemove = umb.ImportNode(areaKey, true);
                                            findAreaInUmbracoLang.RemoveChild(keyToRemove);
                                        }
                                    }
                                }

                                if (!area.HasChildNodes)
                                {
                                    var areaToRemove = umb.ImportNode(area, true);
                                    udrive.DocumentElement.RemoveChild(areaToRemove);
                                }
                            }
                        }

                        umb.Save(lang.FullName);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Error<TranslationHelper>("Failed to remove UDrive localization values to language file", ex);
                }
            }
        }
    }
}
