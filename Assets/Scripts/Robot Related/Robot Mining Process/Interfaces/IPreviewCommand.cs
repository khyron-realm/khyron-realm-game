using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPreviewCommand<T>
{
    public void PreviewCommand(Direction dir);
    public GameObject CommandBlock { set; }
    public List<T> TilesPositions { get; set;}
    public Direction Direction { get; set; }
}
