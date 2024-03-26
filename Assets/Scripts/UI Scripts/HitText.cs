using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HitText : MonoBehaviour
{
    private string text;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private Canvas canvas;
    [SerializeField] private float speed;
    private GameObject cam;
    public string Text { get => text; set { text = value; counter.text = text; } }
    public HitText(string text) {
        this.text = text;
        Debug.Log("bruh");
    }
    // Start is called before the first frame update
    void Start() {
        cam = GameObject.FindGameObjectWithTag("Camera");
        //player = GameManager.GetManager().Zend;
        Destroy(gameObject, 0.5f);
    }
    // Update is called once per frame
    void Update() {
        Vector3 direction =  transform.position- cam.transform.position;
        Quaternion qTo;
        qTo = Quaternion.LookRotation(direction);
        transform.rotation = qTo;
        transform.localPosition += new Vector3(0, speed, 0) * Time.deltaTime;
    }
}
