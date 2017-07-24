using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Complete
{
    public class BasicAI : MonoBehaviour
    {


        private bool aiActive;
        private NavMeshAgent agent;
        private GameObject targetGO;
        private Transform target;
        public Transform eyes;
        public Transform playerEyes;
        public float MoveSpeed = 4f;
        public float MaxDist = 10f;
        public float MinDist = 5f;
        bool playerShoot = false;
        void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }
        // Use this for initialization
        void Start()
        {
            playerShoot = false;
            targetGO = GameObject.FindGameObjectWithTag("Player");
            if (targetGO != null)
                target = targetGO.transform;
            else
                Debug.Log("cant find player");
        }

        // Update is called once per frame
        void Update()
        {
            Debug.DrawRay(eyes.position, eyes.forward.normalized * 10f, Color.blue);
            if (!aiActive)
                return;
            
            transform.LookAt(target);
            agent.SetDestination(target.position);

            if (Vector3.Distance(transform.position, target.position) >= MinDist)
            {


                if (Vector3.Distance(transform.position, target.position) <= MaxDist)
                {
                    //Here Call any function U want Like Shoot at here or something
                    shoot();
                }


            }

            playerShooting();
        }

        void playerShooting(){
            if (!playerShoot)
                return;
            targetGO.GetComponent<TankShooting>().Fire(10,1);
        }
        private void OnDrawGizmos()
        {
            if(eyes!=null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(eyes.position, 2f);
            }
        }

        void shoot()
        {
            RaycastHit hit;

            Debug.DrawRay(eyes.position, eyes.forward.normalized * 10f,Color.red);
            if(Physics.SphereCast(eyes.position, 5f, eyes.forward, out hit, 10f) && hit.collider.CompareTag("Player"))
            {
                GetComponent<TankShooting>().Fire(10f, 1);
            }


        }


        public void SetupAI(bool aiActivationFromTankManager)
        {
            aiActive = aiActivationFromTankManager;
            if (aiActive)
            {
                agent.enabled = true;
            }
            else
            {
                agent.enabled = false;
            }

        }


        public void setGazeAt(bool val)
        {
            //Debug.Log("gazed :"+val);

            playerShoot = val;
        
        }

    }


}
