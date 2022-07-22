using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BotSee
{
    /// <summary>
    /// возвращает true, если увидели хотя бы одну "видимую" точку
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="unit"></param>
    /// <param name="from"></param>
    /// <param name="angle"></param>
    /// <param name="distance"></param>
    /// <param name="mask"></param>
    /// <returns></returns>
    public static bool IsVisibleUnit<T>(T unit, Transform from, float angle, float distance, LayerMask mask) where T : Player
    {
        bool result = false;
        if (unit != null)
        {
            foreach (Transform visiblePoint in unit.visiblePoints)
            {
                if (IsVisibleObject(from, visiblePoint.position, unit.gameObject, angle, distance, mask))
                {
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
    /// <summary>
    /// посылает луч, если луч падает на нужный нам gameObject, то возвращает true
    /// </summary>
    /// <param name="from"></param>
    /// <param name="point"></param>
    /// <param name="target"></param>
    /// <param name="angle"></param>
    /// <param name="distance"></param>
    /// <param name="mask"></param>
    /// <returns></returns>
    public static bool IsVisibleObject(Transform from, Vector3 point, GameObject target, float angle, float distance, LayerMask mask)
    {
        bool result = false;
        if (IsAvailablePoint(from, point, angle, distance))
        {
            Vector3 direction = (point - from.position);
            Ray ray = new Ray(from.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance, mask.value))
            {
                if (hit.collider.gameObject == target)
                {
                    result = true;
                }
            }
        }
        return result;
    }

    /// <summary>
    /// Учитывает угол обзора и возвращает true если угол позволяет увидеть объект
    /// </summary>
    /// <param name="from"></param>
    /// <param name="point"></param>
    /// <param name="angle"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static bool IsAvailablePoint(Transform from, Vector3 point, float angle, float distance)
    {
        bool result = false;

        if (from != null && Vector3.Distance(from.position, point) <= distance)
        {
            Vector3 direction = (point - from.position);
            float dot = Vector3.Dot(from.forward, direction.normalized);
            if (dot < 1)
            {
                float angleRadians = Mathf.Acos(dot);
                float angleDeg = angleRadians * Mathf.Rad2Deg;
                result = (angleDeg <= angle);
            }
            else
            {
                result = true;
            }
        }
        return result;
    }
}
