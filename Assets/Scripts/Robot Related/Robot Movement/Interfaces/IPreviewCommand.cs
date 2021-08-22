using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPreviewCommand<T>
{
    public void PreviewCommand(Direction dir);
    public GameObject CommandBlock { set; }
    public List<T> HitsPreview{ get; set;}
    public Direction Direction { get; set; }
}
