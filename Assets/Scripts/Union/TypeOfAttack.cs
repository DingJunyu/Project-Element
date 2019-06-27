using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfAttack
{
    public class Shape {
        public enum ShapeType {
            rayOfLight,
            radialLight,
            annulus
        }

        int rayOfLight;
        int radialLight;
        int annulus;

        int maxOne;
        int maxName;

        public int RefermaxName() { return maxName; }

        public Shape() {
            rayOfLight = 1;
            radialLight = 0;
            annulus = 0;

            maxOne = rayOfLight;
            maxName = (int)ShapeType.rayOfLight;
        }

        //新しいものを入れる、最大値を更新する
        public bool inputNew(int thisType, int cap) {
            switch (thisType) {
                case (int)ShapeType.rayOfLight: rayOfLight += cap;
                    if (rayOfLight > maxOne) {
                        maxOne = rayOfLight;
                        maxName = (int)ShapeType.rayOfLight;
                    }
                    break;
                case (int)ShapeType.radialLight: radialLight += cap;
                    if (radialLight > maxOne) {
                        maxOne = radialLight;
                        maxName = (int)ShapeType.radialLight;
                    }
                    break;
                case (int)ShapeType.annulus: annulus += cap;
                    if (annulus > maxOne) {
                        maxOne = annulus;
                        maxName = (int)ShapeType.annulus;
                    } break;
                default: return false;
            }
            return true;
        }
    }

    public class SpeedAndSteps {
        public enum TypeOfStep {
            moment,
            stage,
            diffusion
        }

        int moment;//瞬間
        int stage;//パー　パー　パーな感じ～
        int diffusion;//拡散

        float speed;

        int maxOne;
        int maxName;

        public SpeedAndSteps() {
            moment = 1;
            stage = 0;
            diffusion = 0;

            maxOne = 1;
            maxName = (int)TypeOfStep.moment;
        }

        public bool inputNew(int str, int cap, float spe) {
            switch (str) {
                case (int)TypeOfStep.moment: moment += cap;
                    if (moment > maxOne) {
                        maxOne = moment;
                        maxName = (int)TypeOfStep.moment;
                    }
                    break;
                case (int)TypeOfStep.stage:
                    stage += cap;
                    if (stage > maxOne)
                    {
                        maxOne = stage;
                        maxName = (int)TypeOfStep.stage;
                    }
                    break;
                case (int)TypeOfStep.diffusion:
                    diffusion += cap;
                    if (diffusion > maxOne)
                    {
                        maxOne = diffusion;
                        maxName = (int)TypeOfStep.diffusion;
                    }
                    break;
            }
            return true;
        }
    }

    bool isFog = false;//fogの材料を入れると一定確率でfogになれる
    SpeedAndSteps speedAndSteps;
    Shape shape;
    int time;

    public TypeOfAttack() {
        speedAndSteps = new SpeedAndSteps();
        shape = new Shape();
    }

    public void InputNewMaterials(Material material){

    }
}
