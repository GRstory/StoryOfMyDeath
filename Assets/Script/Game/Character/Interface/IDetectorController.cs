using System.Collections.Generic;

public interface IDetectorController
{
    List<Detector> DetectorList { get; set; }
    public void UpdateDetector();
}
