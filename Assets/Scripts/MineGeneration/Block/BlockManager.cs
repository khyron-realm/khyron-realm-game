using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private bool _broken;
    private bool _discovered = false;

    private Material _hiddenSprite;
    private Material _visibleSprite;

    public Material HiddenSprite
    {
        set
        {
            _hiddenSprite = value;
        }
    }
    public Material VisibleSprite
    {
        set
        {
            _visibleSprite = value;
        }
    }
    public bool Broken
    {
        get
        {
            return _broken;
        }
    }

    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;

    private BrokenSprites _brokenSprites;

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();

        _meshRenderer.material = _hiddenSprite;
        //GetComponent<IHealth>().OnHealthZero += DoSmth;
        _broken = false;
    }

    //private void FixedUpdate()
    //{
    //    if (Input.GetKey("up"))
    //    {
    //        _broken = !_broken;
    //        if (_broken)
    //        {
    //            _spriteComponent.sprite = _visibleSprite;
    //            _spriteComponent.color = Color.white;
    //        }
    //        else
    //        {
    //            _spriteComponent.sprite = _hiddenSprite;
    //            _spriteComponent.color = new Color(0.8f, 0.8f, 0.8f);
    //        }
    //    }
    //}

    public void SetBroken()
    {
        _broken = true;
    }

    private void SetDiscovered()
    {
        SetSpriteToVisible();
        _discovered = true;
    }

    private void SetSpriteToVisible()
    {
        _meshRenderer.material = _visibleSprite;
    }



    private void DoSmth()
    {
        SetBroken();
        TriggerSprite(false);
    }

    public void TriggerSprite(bool once)
    { 
        _brokenSprites = ScriptableObjectsAcces.brokenSprites;
        

        Collider2D hit1 = Physics2D.OverlapPoint(new Vector2(transform.position.x + 1.077f, transform.position.y));
        Collider2D hit2 = Physics2D.OverlapPoint(new Vector2(transform.position.x - 1.077f, transform.position.y));
        Collider2D hit3 = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y + 1.077f));
        Collider2D hit4 = Physics2D.OverlapPoint(new Vector2(transform.position.x, transform.position.y - 1.077f));

        List<Collider2D> list = new List<Collider2D> {hit1, hit4, hit2, hit3};

        int code = 0 ;

        if (_broken == true)
        {
            for (int i = 0; i < 4; i++)
            {
                if (list[i].GetComponent<BlockManager>().Broken == false)
                {
                    code += (int)Math.Pow(10, i);
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (list[i].GetComponent<BlockManager>().Broken == true)
                {
                    code += (int)Math.Pow(10, i);
                }
            }
        }
          

        if (once == false)
        {
            foreach (Collider2D item in list)
            {
                item.GetComponent<BlockManager>().TriggerSprite(true);
            }
        }


        if(_broken == true)
        {
            SpritesToChoose(code);
        }
        else
        {
            //SpritesToChoose(code);
        }
    }

    private void SpritesToChoose(int code)
    {
        switch (code)
        {
            case 1100:
                _meshFilter.mesh = _brokenSprites.cornerUpLeft;
                break;
            case 0110:
                _meshFilter.mesh = _brokenSprites.cornerDownLeft;
                break;
            case 0011:
                _meshFilter.mesh = _brokenSprites.cornerDownRight;
                break;
            case 1001:
                _meshFilter.mesh = _brokenSprites.cornerUpRight;
                break;

            case 1110:
                _meshFilter.mesh = _brokenSprites.halfLeft;
                break;
            case 0111:
                _meshFilter.mesh = _brokenSprites.halfDown;
                break;
            case 1011:
                _meshFilter.mesh = _brokenSprites.halfRight;
                break;
            case 1101:
                _meshFilter.mesh = _brokenSprites.halfUp;
                break;

            case 1111:
                _meshFilter.mesh = _brokenSprites.whole;
                break;

            default:
                if (_broken == true)
                {
                    _meshFilter.mesh = null;
                }

                break;
        }
    }
}