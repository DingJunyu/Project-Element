using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfAttack {
    public class Shape {
        public enum ShapeType {
            rayOfLight,
            radialLight,
            annulus
        }

        public static readonly string[] shapeTypeName = {
            "光線",
            "射線",
            "円環"
        };

        int rayOfLight;
        int radialLight;
        int annulus;

        int maxOne;
        int maxName;

        public int ReferMaxName() { return maxName; }
        public int ReferMaxOne() { return maxOne; }

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
                case (int)ShapeType.rayOfLight:
                    rayOfLight += cap;
                    if (rayOfLight > maxOne) {
                        maxOne = rayOfLight;
                        maxName = (int)ShapeType.rayOfLight;
                    }
                    break;
                case (int)ShapeType.radialLight:
                    radialLight += cap;
                    if (radialLight > maxOne) {
                        maxOne = radialLight;
                        maxName = (int)ShapeType.radialLight;
                    }
                    break;
                case (int)ShapeType.annulus:
                    annulus += cap;
                    if (annulus > maxOne) {
                        maxOne = annulus;
                        maxName = (int)ShapeType.annulus;
                    }
                    break;
                default: return false;
            }
            return true;
        }

        void Save(string str) {
            PlayerPrefs.SetInt(str + "rayOfLight", rayOfLight);
        }
    }

    public class SpeedAndSteps {
        public enum TypeOfStep {
            moment,
            stage,
            diffusion
        }

        public static readonly string[] stepTypeName = {
            "瞬間",
            "階段",
            "拡散"
        };

        int moment;//瞬間
        int stage;//パー　パー　パーな感じ～
        int diffusion;//拡散

        float speed;
        public float ReferSpeed() { return speed; }

        int maxOne;
        int maxName;

        public int ReferMaxName() { return maxName; }

        public SpeedAndSteps() {
            moment = 1;
            stage = 0;
            diffusion = 0;

            speed = 0.5f;

            maxOne = 1;
            maxName = (int)TypeOfStep.moment;
        }

        public bool inputNew(int str, int cap, float spe) {
            switch (str) {
                case (int)TypeOfStep.moment:
                    moment += cap;
                    if (moment > maxOne) {
                        maxOne = moment;
                        maxName = (int)TypeOfStep.moment;
                        speed += spe;
                    }
                    break;
                case (int)TypeOfStep.stage:
                    stage += cap;
                    if (stage > maxOne) {
                        maxOne = stage;
                        maxName = (int)TypeOfStep.stage;
                        speed += spe;
                        speed *= 0.95f;
                    }
                    break;
                case (int)TypeOfStep.diffusion:
                    diffusion += cap;
                    if (diffusion > maxOne) {
                        maxOne = diffusion;
                        maxName = (int)TypeOfStep.diffusion;
                        speed += spe;
                        speed *= 0.8f;
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

    public void InputNewMaterials(Material material) {
        shape.inputNew(material.ReferShapeType(), material.ReferStrength());
        speedAndSteps.inputNew(material.ReferStepType(), material.ReferStrength(),
            material.ReferSpeed());
        if (material.ReferFog() && !isFog) {
            if (Random.Range(0, 100) > 60)
                isFog = true;
        }
    }

    public string ReferAttackType() {
        return Shape.shapeTypeName[shape.ReferMaxName()] + " " +
            SpeedAndSteps.stepTypeName[speedAndSteps.ReferMaxName()];
    }

    public bool ReferFog() {
        return isFog;
    }

    public float ReferSpeed() {
        return speedAndSteps.ReferSpeed();
    }

    public void save() {

    }
}
