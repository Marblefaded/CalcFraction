/*using Microsoft.AspNetCore.Components;
using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;

namespace CalcFraction.Pages
{
    public class QrGeneratorView: ComponentBase
    {
        public string QRCodeText { get; set; }
        public string QRByte = "";

        public void GenerateQRCode()
        {
            if (!string.IsNullOrEmpty(QRCodeText))
            {
                using MemoryStream ms = new();
                QRCodeGenerator qrCodeGenerate = new();
                QRCodeData qrCodeData = qrCodeGenerate.CreateQrCode(QRCodeText, QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new(qrCodeData);
                using Bitmap qrBitMap = qrCode.GetGraphic(20);
                qrBitMap.Save(ms, ImageFormat.Png);
                string base64 = Convert.ToBase64String(ms.ToArray());
                QRByte = string.Format("data:image/png;base64,{0}", base64);
            }
        }
    }
}
*/