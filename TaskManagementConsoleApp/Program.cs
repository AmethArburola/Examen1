using TaskManagementLibrary; //RETO 1 (1 pt): La línea está bien, pero no funciona, corregir

class Program
{
    static bool confirme(string accion)
    {
        Console.WriteLine("Confirme " + accion + " s/n");
        return Console.ReadLine() == "s";
    } 

    static void Main(string[] args)
    {
        var taskService = new TaskService();

        while (true)
        {
            Console.WriteLine("1. Agregar tarea");
            Console.WriteLine("2. Ver tareas");
            Console.WriteLine("3. Actualizar tarea");
            Console.WriteLine("4. Eliminar tarea");
            Console.WriteLine("5. Completar tarea");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Console.Write("Titulo: ");
                    var title = Console.ReadLine().Trim();                       
                    Console.Write("Descripcion: ");
                    var description = Console.ReadLine().Trim();

                    // RETO 2 (3 pts): Verificar que los datos no sean solo espacios en blanco
                    title = string.IsNullOrWhiteSpace(title) ? null : title;
                    description = string.IsNullOrWhiteSpace(description) ? null : description;

                    var task = taskService.AddTask(title, description);
                    Console.WriteLine($"Tarea agregada con Id: {task.Id}");
                    break;
                case "2":
                    var tasks = taskService.GetAllTasks();
                    Console.WriteLine("-------------------------------------------------");
                    foreach (var t in tasks)
                    {
                        Console.WriteLine($"ID: {t.Id}, Titulo: {t.Title}, Descripcion: {t.Description}, Completada: {t.IsCompleted}");
                    }
                    Console.WriteLine("-------------------------------------------------");
                    break;
                case "3":
                    Console.Write("Introduzca el Id de la tarea por actualizar: ");
                    var updateId = int.Parse(Console.ReadLine());
                    task = taskService.GetTaskById(updateId); // RETO 3 (2 pts): Cargar la tarea con el id indicado

                    if (task != null)
                    {
                        // RETO 4 (1 pt): Imprimir el título de la tarea seleccionada
                        Console.WriteLine($"Título actual: {task.Title}");
                        
                        Console.Write("-> Nuevo titulo: ");
                        var newTitle = Console.ReadLine();
                        
                        // RETO 5 (1 pt): Imprimir la descripción de la tarea seleccionada
                        Console.WriteLine($"Descripción actual: {task.Description}");
                        
                        Console.Write("-> Nueva Descripcion: ");
                        var newDescription = Console.ReadLine();
                        
                        Console.Write("Completada (true/false): ");
                        var isCompleted = bool.Parse(Console.ReadLine());
                        
                        // RETO 6 (5 pts): Modificar la librería para que no se modifique si title o description son vacíos
                        if (taskService.UpdateTask(updateId, newTitle, newDescription, isCompleted))
                        {
                            Console.WriteLine("Tarea actualizada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("Tarea no encontrada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "4":
                    Console.Write("Introduzca el Id de la tarea a eliminar: ");
                    var deleteId = 0;
                    try
                    {
                        deleteId = int.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        break;
                    }
                    
                    task = taskService.GetTaskById(deleteId); // RETO 7 (2 pts): La línea está bien, pero por alguna razón no se puede realizar el llamado de la función
                    
                    if (task != null)
                    {
                        Console.WriteLine("Tarea:");
                        Console.Write("     - ");
                        Console.WriteLine(task.Title);
                        
                        if (confirme("eliminar"))
                        {
                            if (taskService.DeleteTask(deleteId))
                            {
                                Console.WriteLine("Tarea eliminada exitosamente.");
                            }
                            else
                            {
                                Console.WriteLine("Tarea no encontrada.");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "5":
                    // RETO 8 (5 pts): Crear la funcionalidad completa del método
                    Console.Write("Introduzca el Id de la tarea a completar: ");
                    var completeId = int.Parse(Console.ReadLine());
                    
                    task = taskService.GetTaskById(completeId);
                    
                    if (task != null)
                    {
                        Console.WriteLine($"Tarea: {task.Title}");
                        
                        if (taskService.CompleteTask(completeId))
                        {
                            Console.WriteLine("Tarea completada exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo completar la tarea.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tarea no encontrada.");
                    }
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción inválida, intente de nuevo.");
                    break;
            }
        }
    }
}
