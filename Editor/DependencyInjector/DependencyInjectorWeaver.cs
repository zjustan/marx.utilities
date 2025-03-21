using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using UnityEngine;
using Weaver;
using Weaver.Extensions;
using Object = UnityEngine.Object;

namespace Marx.Utilities.Editor
{
    public class DependencyInjectorWeaver : WeaverComponent
    {
        public override string ComponentName => "Dependency Injector";

        public override DefinitionType EffectedDefintions => DefinitionType.Type;
        
        public override void VisitType(TypeDefinition typeDefinition)
        {
            bool requiresInjection = typeDefinition.Fields.Cast<ICustomAttributeProvider>()
                .Concat(typeDefinition.Properties)
                .Any(x => x.HasCustomAttribute<InjectAttribute>()); 
            
            if(!requiresInjection)
                return;

            if (typeDefinition.InheritsFrom<Object>())
            {
                string executingMethod = "Awake";

                if (typeDefinition.InheritsFrom<ScriptableObject>())
                    executingMethod = "OnEnable";
            
                MethodDefinition method = typeDefinition.GetMethod(executingMethod);
                if (method != null)
                {
                    InjectServiceCall(method);
                    return;
                }
            
                method = new MethodDefinition(executingMethod, MethodAttributes.Public | MethodAttributes.HideBySig, typeDefinition.Module.TypeSystem.Void);
                typeDefinition.Methods.Add(method);
                InjectServiceCall(method);
                return;
            }
        

            foreach (var constructor in typeDefinition.Methods.Where(x => x.IsConstructor && !x.IsStatic))
            {
                InjectServiceCall(constructor);
            }
            base.VisitType(typeDefinition);
        }
    
        private static void InjectServiceCall(MethodDefinition method)
        {
            var processor = method.Body.GetILProcessor();
            method.Module.ImportReference(typeof(DependencyInjector));
            var serviceMethod = method.Module.ImportReference(typeof(DependencyInjector).GetMethod("Inject"));

            Instruction LoadThisInstruction = processor.Create(OpCodes.Ldarg_0);
            Instruction CallInjectorInstruction = processor.Create(OpCodes.Call, serviceMethod);
            
            var firstInstruction = method.Body.Instructions.FirstOrDefault();
            if (firstInstruction == null)
            {
                processor.Append(LoadThisInstruction);
                processor.Append(CallInjectorInstruction);
                processor.Append(processor.Create(OpCodes.Ret)); // Return
                return;
            }
            processor.InsertBefore(firstInstruction, LoadThisInstruction);
            processor.InsertBefore(firstInstruction, CallInjectorInstruction);
        }
    
    }
}