using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineRemake : MonoBehaviour
{
    public float bounciness = 0.8f; // �����W���̒l
    private PhysicMaterialCombine frictionCombine = PhysicMaterialCombine.Maximum; // ���C�������[�h�̒l
    private PhysicMaterialCombine bounceCombine = PhysicMaterialCombine.Maximum; // �����������[�h�̒l

    private new Collider collider;
    private new Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        this.collider = this.GetComponent<Collider>();
        this.rigidbody = this.GetComponent<Rigidbody>();

        var newMaterial = new PhysicMaterial();

        newMaterial.bounciness = bounciness;
        newMaterial.frictionCombine = frictionCombine;
        newMaterial.bounceCombine = bounceCombine;

        this.collider.sharedMaterial = newMaterial;
    }
}
