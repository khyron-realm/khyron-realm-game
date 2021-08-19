using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPreviewCommand
{
    public void PreviewCommand(Direction dir);
    public GameObject CommandBlock { set; }
    public List<Collider2D> HitsPreview{ get; set;}
    public Direction Direction { get; set; }
}
