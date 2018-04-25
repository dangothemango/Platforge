using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platforge{

    public class ForgeAssembly : ForgeComponent {

        public class Number : UnityEngine.Object {

            public float value;

            public Number(float v) {
                this.value = v;
            }
        }

        public class String : UnityEngine.Object {

            public string value;

            public String(string v) {
                this.value = v;
            }

        }

        public class Bool : UnityEngine.Object {

            public bool value;

            public Bool(bool v) {
                this.value = v;
            }
        }

        public abstract class Command {

            public abstract void Run(UnityEngine.Object[] registers);

        }

        public class SetGameobjectActive : Command {

            public int IsActiveReference;
            public int ObjectReference;

            public override void Run(UnityEngine.Object[] registers) {
                Bool isActive = registers[IsActiveReference] as Bool;
                GameObject go = registers[ObjectReference] as GameObject;
                go.SetActive(isActive);
            }

        }

        public class DestroyGameObject : Command {

            public int ObjectReference;

            public override void Run(UnityEngine.Object[] registers) {
                GameObject go = registers[ObjectReference] as GameObject;
                Destroy(go);
            }

        }

        public class ArithmaticExpression : Command {
            public int LeftReference;
            public int RightReference;
            public int ResultReference;

            public override void Run(UnityEngine.Object[] registers) {
                throw new Exception("Cannot use base class ArithmaticExpression");
            }

            public void Configure(int lr, int rr, int RR) {
                LeftReference = lr;
                RightReference = rr;
                ResultReference = RR;
            }
        }

        public class Subtract : ArithmaticExpression {

            public override void Run(UnityEngine.Object[] registers) {
                registers[ResultReference] = new Number((registers[LeftReference] as Number).value - (registers[RightReference] as Number).value);
            }

        }

        public class Add : ArithmaticExpression {

            public override void Run(UnityEngine.Object[] registers) {
                registers[ResultReference] = new Number((registers[LeftReference] as Number).value + (registers[RightReference] as Number).value);
            }

        }

        public class Multiply : ArithmaticExpression {

         

            public override void Run(UnityEngine.Object[] registers) {
                registers[ResultReference] = new Number((registers[LeftReference] as Number).value * (registers[RightReference] as Number).value);
            }

        }

        public class Divide : ArithmaticExpression {

            public override void Run(UnityEngine.Object[] registers) {
                registers[ResultReference] = new Number((registers[LeftReference] as Number).value % (registers[RightReference] as Number).value);
            }

        }

        public class ConditionalExpression : Command {
            public int CompareReference;
            public int ResultReference;

            public override void Run(UnityEngine.Object[] registers) {
                throw new Exception("Cannot use base class ConditionalExpression");
            }

            public void Configure(int cr, int RR) {
                CompareReference = cr;
                ResultReference = RR;
            }
        }

        public class IsLessThanZero : ConditionalExpression {

            public override void Run(UnityEngine.Object[] registers) {
                registers[ResultReference] = new Bool((registers[CompareReference] as Number).value < 0);
            }

        }

        public class EqualsZero : ConditionalExpression {

            public override void Run(UnityEngine.Object[] registers) {
                registers[ResultReference] = new Bool((registers[CompareReference] as Number).value == 0);
            }

        }

        public static void RunCommandList(List<Command> list) {
            UnityEngine.Object[] registers = new UnityEngine.Object[10];

            foreach (Command c in list) {
                c.Run(registers);
            }
        }


    }
}
