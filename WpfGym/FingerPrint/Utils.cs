using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xceed.Wpf.DataGrid.Markup;

namespace WpfGym.FingerPrint
{
    public class Utils
    {

        //private DPFP.Capture.Capture Capturer;

        //public DPFP.FeatureSet ExtractFeatures(DPFP.Sample Sample, DPFP.Processing.DataPurpose Purpose)
        //{
        //    DPFP.Processing.FeatureExtraction Extractor = new DPFP.Processing.FeatureExtraction();  // Create a feature extractor
        //    DPFP.Capture.CaptureFeedback feedback = DPFP.Capture.CaptureFeedback.None;
        //    DPFP.FeatureSet features = new DPFP.FeatureSet();
        //    Extractor.CreateFeatureSet(Sample, Purpose, ref feedback, ref features);            // TODO: return features as a result?
        //    if (feedback == DPFP.Capture.CaptureFeedback.Good)
        //        return features;
        //    else
        //        return null;

        //}

        //public void Stop()
        //{
        //    if (null != Capturer)
        //    {
        //        try
        //        {
        //            Capturer.StopCapture();
        //        }
        //        catch(Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}

        //public void Start()
        //{
        //    if (null != Capturer)
        //    {
        //        try
        //        {
        //            Capturer.StartCapture();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}

        //#region EventHandler Members:

        //public void OnComplete(object Capture, string ReaderSerialNumber, DPFP.Sample Sample)
        //{
        //    //MakeReport("The fingerprint sample was captured.");
        //    //SetPrompt("Scan the same fingerprint again.");
        //    Process(Sample);
        //}

        //public void OnFingerGone(object Capture, string ReaderSerialNumber)
        //{
        //    //MakeReport("The finger was removed from the fingerprint reader.");
        //}

        //public void OnFingerTouch(object Capture, string ReaderSerialNumber)
        //{
        //    //MakeReport("The fingerprint reader was touched.");
        //}

        //public void OnReaderConnect(object Capture, string ReaderSerialNumber)
        //{
        //    //MakeReport("The fingerprint reader was connected.");
        //}

        //public void OnReaderDisconnect(object Capture, string ReaderSerialNumber)
        //{
        //   // MakeReport("The fingerprint reader was disconnected.");
        //}

        //public void OnSampleQuality(object Capture, string ReaderSerialNumber, DPFP.Capture.CaptureFeedback CaptureFeedback)
        //{
        //    //if (CaptureFeedback == DPFP.Capture.CaptureFeedback.Good)
        //    //   // MakeReport("The quality of the fingerprint sample is good.");
        //    //else
        //        //MakeReport("The quality of the fingerprint sample is poor.");
        //}
        //#endregion


        //protected virtual void Process(DPFP.Sample Sample)
        //{
        //    // Draw fingerprint sample image.
        //    DrawPicture(ConvertSampleToBitmap(Sample));
        //}

        //private void DrawPicture(Bitmap bitmap)
        //{
        //    this.Invoke(new Function(delegate () {
        //        Picture.Image = new Bitmap(bitmap, Picture.Size);   // fit the image into the picture box
        //    }));
        //}

    }
}
