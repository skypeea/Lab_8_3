using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_8_3
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            using (var ts = new Transaction(doc, "ImageExport"))
            {
                ts.Start();
                string desktop_path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

                View view = doc.ActiveView;

                string filepath = Path.Combine(desktop_path, view.Name);

                ImageExportOptions options = new ImageExportOptions();
                options.FilePath = filepath;
                options.ExportRange = ExportRange.CurrentView;
                options.ZoomType = ZoomFitType.FitToPage;
                options.ImageResolution = ImageResolution.DPI_300;
                options.PixelSize = 800;
                options.FitDirection = FitDirectionType.Horizontal;
                options.HLRandWFViewsFileType = ImageFileType.PNG;
                options.ShadowViewsFileType = ImageFileType.PNG;
                
                doc.ExportImage(options);

                ts.Commit();
            }

            return Result.Succeeded;

        }
    }
}
