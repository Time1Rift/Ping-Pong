using UnityEngine;
using UnityEngine.SceneManagement;

public class Predictor : MonoBehaviour
{
    [SerializeField] private Rigidbody _ball;
    [SerializeField] private GameObject _table;

    [Header("Simulation Settings")]
    [SerializeField, Min(0)] private float _step = 0.02f;
    [SerializeField, Min(0)] private int _countStep = 50;   // 90
    [SerializeField] private Transform _endPoint;

    [SerializeField] private Transform _phantomPrefab;

    private Scene _scene;
    private PhysicsScene _simulationScene;
    private Rigidbody _simulationBodyBall;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (_endPoint == null)
            return;

        Gizmos.DrawSphere(_endPoint.position, 0.1f);
    }

    public void Prepare()
    {
        _scene = SceneManager.CreateScene("Physics simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _simulationScene = _scene.GetPhysicsScene();

        _simulationBodyBall = Instantiate(_ball);
        //_simulationBody.GetComponent<MeshRenderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(_simulationBodyBall.gameObject, _scene);

        var table = Instantiate(_table, _table.transform.position, _table.transform.rotation);
        _simulationBodyBall.GetComponent<MeshRenderer>().enabled = false;
        SceneManager.MoveGameObjectToScene(table, _scene);
    }

    public Vector3 Predict(bool isPlayerSide, out float time)
    {
        Vector3 finalPosition = Vector3.zero;

        time = 0;
        _simulationBodyBall.transform.position = _ball.transform.position;
        _simulationBodyBall.velocity = _ball.velocity;

        for (int i = 0; i < _countStep; i++)
        {
            _simulationScene.Simulate(_step);
            time += _step;
            Instantiate(_phantomPrefab, _simulationBodyBall.position, Quaternion.identity);

            if (_simulationBodyBall.position.z < _endPoint.position.z)
            {                
                finalPosition = _simulationBodyBall.position;
                break;
            }
        }

        return finalPosition;
    }
}