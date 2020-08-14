using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TorusSpawner : MonoBehaviour
{
    [SerializeField]
    private CreateTorus torus;
    [SerializeField]
    private ParticleSystem spawnEff;

    [SerializeField]
    private Rect spawnRange;

    [SerializeField]
    private float spawnInterval = 0.5f;

    [SerializeField]
    private float spwanAccel = 0.01f;

    private float timeScale = 1.0f;
    private float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //timeScale -= spwanAccel * Time.deltaTime;

        Time.timeScale += spwanAccel * Time.deltaTime;

        timer += Time.deltaTime;

        if(timer > spawnInterval)
        {
            timer -= spawnInterval;
            StartCoroutine(CreateTorus());
        }
    }

    private IEnumerator CreateTorus()
    {
        Vector3 pos = new Vector3(Random.Range(spawnRange.x, spawnRange.xMax), -1.75f, Random.Range(spawnRange.y, spawnRange.yMax));

        var eff = Instantiate(spawnEff);
        eff.transform.position = pos;

        yield return new WaitForSeconds(1.0f);

        Destroy(eff.gameObject);

        Instantiate(torus, pos, Quaternion.identity);
    }
}
