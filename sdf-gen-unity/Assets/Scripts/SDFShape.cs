﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class SDFShape : MonoBehaviour
{
    public enum ShapeType
    {
        None = 0,
        Plane = 1,
        Sphere = 2,
        Cube = 3,
        Cylinder = 4,
        Mesh = 5,
    }

    public ShapeType shapeType = ShapeType.Sphere;

    private MeshFilter filter;
    private MeshRenderer meshRenderer;
    private ShapeType prevType = ShapeType.Sphere;

    private void Awake()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();
        this.filter = GetComponent<MeshFilter>();
        this.prevType = shapeType;

        this.meshRenderer.sharedMaterial = new Material(Shader.Find("Particles/Alpha Blended"));
        this.meshRenderer.sharedMaterial.SetColor("_TintColor", new Color(0f, 0f, 0f, 0f));

        RebuildMesh();
    }

    protected void Update()
    {
        if (prevType != shapeType)
            RebuildMesh();

        this.name = shapeType.ToString();
        prevType = shapeType;
    }

    private void RebuildMesh()
    {
        PrimitiveType t = PrimitiveType.Sphere;

        switch (shapeType)
        {
            case ShapeType.None:
                break;
            case ShapeType.Plane:
                t = PrimitiveType.Plane;
                break;
            case ShapeType.Sphere:
                t = PrimitiveType.Sphere;
                break;
            case ShapeType.Cube:
                t = PrimitiveType.Cube;
                break;
            case ShapeType.Cylinder:
                t = PrimitiveType.Cylinder;
                break;
            case ShapeType.Mesh:
                break;
        }

        GameObject go = GameObject.CreatePrimitive(t);
        this.filter.sharedMesh = go.GetComponent<MeshFilter>().sharedMesh;
        GameObject.DestroyImmediate(go);
    }

    public Vector4 GetParameters()
    {
        float type = ((int)shapeType);

        switch (shapeType)
        {
            case ShapeType.None:
                break;
            case ShapeType.Plane:
                return new Vector4(transform.up.x, transform.up.y, transform.up.z, type);
            case ShapeType.Sphere:
                return Vector4.one * type;
            case ShapeType.Cube:
                return Vector4.one * type;
            case ShapeType.Cylinder:
                return Vector4.one * type;
            case ShapeType.Mesh:
                break;
        }

        return Vector4.zero;
    }
}