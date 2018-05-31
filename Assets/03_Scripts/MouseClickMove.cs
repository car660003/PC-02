using UnityEngine;
using System.Collections;

public class MouseClickMove : MonoBehaviour {

	public GameObject model;
	private bool moveState = false;
	private Vector3 target = new Vector3();
	public float speed = 1;

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);////(1)使用Unity內Ray變數將Camera位置到滑鼠位置轉換成一條3D射線，
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit)){////(2)將射線投到物件上，這裡使用物件的Tag名稱以及是否按下滑鼠左鍵作為判斷，
			if (Input.GetMouseButtonDown (0) && hit.transform.gameObject.tag == "floor"){
				moveState = true;////(3)若上述條件成立則儲存移動目的點的位置至變數target，並且將移動狀態的布林變數moveState設為true，
				target = new Vector3 (hit.point.x, hit.point.y, hit.point.z);
			}
		}
		float step = speed * Time.deltaTime;////(4)移動速度使用Time.deltaTime，以避免不同效能的電腦移動速度不一樣，
		////(7)計算出模型到目標點的向量，使用Vector3.RotateTowards算出每次Update時需要旋轉多少度，存到newDir變數內，最後透過Quaternion.LookRotation實際去旋轉就完成了。
		Vector3 targetDir = target - model.transform.position;
		Vector3 newDir = Vector3.RotateTowards(model.transform.forward, targetDir, step*10, 0.0F);
		model.transform.rotation = Quaternion.LookRotation(newDir);

		if(moveState){////(5)判斷moveState，當成立時才做移動的動作，並且在模型與目標點小於一定範圍時將 moveState設為false，
			if(Vector3.Distance(model.transform.position,target)<0.1f){
				moveState = false;
			}
			model.transform.position = Vector3.MoveTowards(model.transform.position,target,step);////(6)移動時使用Vector3.MoveTowards，將模型(model)移動到目標點(target)上。

		}
	}
}