using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    float vertexOffsets = 50;
    [SerializeField]
    float vertexHeightInterval = 5;
    [SerializeField]
    float mapSizeX = 1000;
    static float MAPSIZEX = 1000;
    [SerializeField]
    float mapSizeZ = 1000;
    static float MAPSIZEZ = 1000;
    const float dropOffHeight = 10;
    Mesh map = null;
    Vector3[] _verteces;

    private void Awake()
    {
        MAPSIZEX = mapSizeX;
        MAPSIZEZ = mapSizeZ;
        GenerateMesh();        
    }

    void GenerateMesh()
    {
        map = new Mesh();
        gameObject.AddComponent<MeshFilter>().mesh = map;
        MeshCollider _meshCollider = gameObject.GetComponent<MeshCollider>();
        _meshCollider.sharedMesh = map;

        const int _nrOfIndecesPerTriangle = 6;
        int _nrOfQuadsX = (int)(MAPSIZEX / vertexOffsets);
        int _nrOfQuadsZ = (int)(MAPSIZEZ / vertexOffsets);
        int _nrOfVertsX = _nrOfQuadsX + 1;
        int _nrOfVertsZ = _nrOfQuadsZ + 1;
        int _nrOfVerts = _nrOfVertsX * _nrOfVertsZ;
        int _nrOfIndeces = _nrOfQuadsX * _nrOfQuadsZ * _nrOfIndecesPerTriangle;

        _verteces = new Vector3[_nrOfVerts];
        
        for (int i = 0; i < _nrOfVerts; ++i)
        {
            _verteces[i] = new Vector3(vertexOffsets * (i % _nrOfVertsX), Random.Range(-vertexHeightInterval, vertexHeightInterval), vertexOffsets * (i / _nrOfVertsZ));
        }
        
        int[] _triangleIndeces = new int[_nrOfIndeces];
        int _currentTriangle = 0;
        int _currentVertex = 0;

        for (int i = 0; i < _nrOfQuadsZ; ++i)
        {
            for (int j = 0; j < _nrOfQuadsX; ++j)
            {
                _triangleIndeces[_currentTriangle] = _currentVertex;
                _triangleIndeces[_currentTriangle + 1] = _currentVertex + _nrOfQuadsX + 1;
                _triangleIndeces[_currentTriangle + 2] = _currentVertex + 1;
                _triangleIndeces[_currentTriangle + 3] = _currentVertex + 1;
                _triangleIndeces[_currentTriangle + 4] = _currentVertex + _nrOfQuadsX + 1;
                _triangleIndeces[_currentTriangle + 5] = _currentVertex + _nrOfQuadsX + 2;
                ++_currentVertex;
                _currentTriangle += _nrOfIndecesPerTriangle;
            }
            ++_currentVertex;
        }

        map.vertices = _verteces;
        map.triangles = _triangleIndeces;
        map.RecalculateNormals();
        map.RecalculateBounds();
        _meshCollider.convex = true;
        _meshCollider.convex = false;
    }

    static public Vector3 GetRandomDropOffPoint()
    {
        return new Vector3(Random.Range(0.0f, MAPSIZEX), dropOffHeight, Random.Range(0.0f, MAPSIZEZ));
    }

    static public bool IsInMap(float _xPos, float _zPos)
    {
        return _xPos > 0 && _zPos > 0 && _xPos < MAPSIZEX && _zPos < MAPSIZEZ;
    }
}
