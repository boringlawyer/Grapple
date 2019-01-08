using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [ExecuteInEditMode]
 public class DuplicateCatcher : MonoBehaviour 
 {
     [SerializeField] int instanceID = 0;
     void Awake(){
         if (instanceID != GetInstanceID()){
             if (instanceID == 0){
                 instanceID = GetInstanceID();
             }
             else {
                 instanceID = GetInstanceID();
                 if (instanceID < 0){
					 GetComponent<ICustomDuplicate>().OnDuplicate();
                 }
             }
         }
     }
 }
