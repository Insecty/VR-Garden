using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

//这个任务脚本的作用就是控制游戏物体到达目标位置
public class MySeek : Action
{//这个任务的调用是有behavior designer行为树控制的

    //public float speed;
    public SharedFloat sharedSpeed;
    public SharedTransform target;//这个是我们要达到的目标位置
    //public float arriveDistance = 0.1f;
    public SharedFloat sharedArriveDistance = 0.1f;
    private float sqrArriveDistance;
    public SharedBool isHPFlee;
    public override void OnStart()
    {
        sqrArriveDistance = sharedArriveDistance.Value * sharedArriveDistance.Value;
    }


    //当进入到这个任务的时候，会一直调用这个方法，一直到任务结束  你返回一个 成功或者失败的状态  那么任务结束 如果返回一个running的状态，那这个方法会继续呗调用
    public override TaskStatus OnUpdate()
    {//这个方法的调用频率，默认是跟unity里面的帧保持一致的
        if (target == null || target.Value == null || isHPFlee.Value == false)
        {
            return TaskStatus.Failure;
        }
        Debug.Log("FLEEING!!!!!!!!!!!!!!!!!!!!!!!!!");
        transform.LookAt(target.Value.position);//直接朝向目标位置
        transform.position = Vector3.MoveTowards(transform.position, target.Value.position, sharedSpeed.Value * Time.deltaTime);
        if ((target.Value.position - transform.position).sqrMagnitude < sqrArriveDistance)
        {
            Debug.Log("结束！！！！！！！！！！！！！！");
            return TaskStatus.Failure;//如果 距离目标位置的距离比较小，认为到达了目标位置，直接return成功
        }
        return TaskStatus.Running;
    }
}