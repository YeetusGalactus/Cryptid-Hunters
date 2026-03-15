using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{

    public NavMeshAgent ai;
    public Transform player;
    public Animator aiAnim;
    public Vector3 dest;
    CharacterController playerController;

    private void Start() {
        playerController = player.GetComponent<CharacterController>();
    }
    private void Update() {
        dest = player.position;
        ai.destination = dest;

        float playerSpeed = playerController.velocity.magnitude;

        if (ai.remainingDistance >= ai.stoppingDistance) {
            if (playerSpeed < 0.1f) {
                ai.speed = 8f;
            }
            if (playerSpeed > 0.3f && playerSpeed < 10f) {
                ai.speed = playerSpeed;
                aiAnim.ResetTrigger("idle");
                aiAnim.ResetTrigger("run");
                aiAnim.SetTrigger("walk");
            } 
            
            else if (playerSpeed > 10f) {
                ai.speed = playerSpeed;
                aiAnim.ResetTrigger("idle");
                aiAnim.ResetTrigger("walk");
                aiAnim.SetTrigger("run");

            } 
            
        } else {
            aiAnim.ResetTrigger("run");
            aiAnim.ResetTrigger("walk");
            aiAnim.SetTrigger("idle");
        }
    }

}
