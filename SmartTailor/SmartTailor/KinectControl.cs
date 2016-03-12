using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using KinectUtils;

namespace SmartTailor;
{
    class KinectControl
    {
        KinectSensor sensor;

        CoordinateMapper coordinateMapper = null;
        MultiSourceFrameReader reader;
        bool bStopAfterFrameInterpolation = false;

        KinectUtils.BackgroundRemoval backgroundRemovalTool;

        private delegate void DispatcherCallback();
        private ImageSource ImageScreenshot;


        private int iFrameInterpolation = 0;
        private int numberOfCutPoints = 0;
        private List<ushort[]> pixelsList;

        public void KinectConnect() {
            sensor = KinectSensor.GetDefault();

            if (sensor != null) {
                sensor.Open();

                reader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.BodyIndex);
                //_reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth);
                reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
                this.ImageScreenshot = new WriteableBitmap(this.sensor.DepthFrameSource.FrameDescription.Width, this._sensor.DepthFrameSource.FrameDescription.Height, 96.0, 96.0, PixelFormats.Gray8, null);
                coordinateMapper = sensor.CoordinateMapper;
                backgroundRemovalTool = new BackgroundRemoval(sensor.CoordinateMapper);
            }
        }
    }
}
