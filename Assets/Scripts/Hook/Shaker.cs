
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


	public class Shaker : MonoBehaviour
	{
                 public Transform MainCamera; // Основная камера (с указанием вибрации всей сцены)
                 public Transform NeedGameObject; // Объект, которому нужна вибрация
                 public SpriteRenderer Obj;
 
                 // Объект перемещается или возвращает идентификатор указанной позиции
        private bool IsMove = false;
 
        private void Update()
        {
                         // Вся сцена вибрирует
            if (Input.GetKeyDown(KeyCode.A))
            {
                MyShake(MainCamera);
            }
                         // Вибрация отдельного объекта
            if (Input.GetKeyDown(KeyCode.B))
            {
                MyShake(NeedGameObject);
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                Obj.DOColor(Color.red, 1f);
            }
        }
 
        public void MyShake( Transform tf)
        {
            if (tf!=null)
            {
                                 // Контроль вибрации
                //Tweener tweener = tf.DOShakePosition(5);
            Tweener tweener = tf.DOShakePosition(1,1);
 
                                 //// Указывает на вибрацию в направлении оси X
                //Tweener tweener = tf.DOShakePosition(11, new Vector3(1, 0, 0), 20);
 
                                 //// означает вибрацию в направлении оси Y
                //Tweener tweener = tf.DOShakePosition(11, new Vector3(0, 1, 0), 20);
            }
            
        }
 
 
 
 
 
    }//class_end
