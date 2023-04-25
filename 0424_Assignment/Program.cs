using System.ComponentModel.Design;

namespace _0424_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            // 기본 int 우선순위(오름차순) 적용
            DataStructure.PriorityQueue<string, int> pq1 = new DataStructure.PriorityQueue<string, int>();

            pq1.Enqueue("데이터1", 1);     // 우선순위와 데이터를 추가
            pq1.Enqueue("데이터2", 3);
            pq1.Enqueue("데이터3", 5);
            pq1.Enqueue("데이터4", 2);
            pq1.Enqueue("데이터5", 4);
            
            while (pq1.Count > 0) Console.WriteLine(pq1.Dequeue()); // 우선순위가 높은 순서대로 데이터 출력

            // 수정 int 우선순위 적용
            DataStructure.PriorityQueue<string, int> pq2 = new DataStructure.PriorityQueue<string, int>(Comparer<int>.Create((a, b) => b - a));

            pq2.Enqueue("데이터1", 1);     // 우선순위와 데이터를 추가
            pq2.Enqueue("데이터2", 3);
            pq2.Enqueue("데이터3", 5);
            pq2.Enqueue("데이터4", 2);
            pq2.Enqueue("데이터5", 4);
            
            while (pq2.Count > 0) Console.WriteLine(pq2.Dequeue()); // 우선순위가 높은 순서대로 데이터 출력
            

            EmergencyRoom<string, int> emergencyRoom = new EmergencyRoom<string, int>();
            emergencyRoom.EnqueuePatient("1번 환자", 10);
            emergencyRoom.EnqueuePatient("2번 환자", 30);
            emergencyRoom.EnqueuePatient("3번 환자", 50);
            emergencyRoom.EnqueuePatient("4번 환자", 5);
            emergencyRoom.EnqueuePatient("5번 환자", 20);

            while(emergencyRoom.Count > 0) { Console.WriteLine(emergencyRoom.DequeuePatient()); }
            */
            Console.WriteLine(FindMedian()); 
        }

        static double FindMedian()
        {
            int arrSize = 10;
            int median = 0;
            double result = 0;
            int[] array = new int[arrSize];
            Random random = new Random();
            for(int i = 0; i < arrSize; i++)
            {
                array[i] = random.Next(0, 100);               
            }
            // 배열 확인 코드
            Array.Sort(array);
            for(int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();

            PriorityQueue<int, int> maxQueue = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b - a));
            PriorityQueue<int, int> minQueue = new PriorityQueue<int, int>();

            median = array[0];
            for(int i = 0; i < arrSize; i++)
            {
                if (array[i] >= median)
                {
                    minQueue.Enqueue(array[i], array[i]);
                }
                else
                {
                    maxQueue.Enqueue(array[i], array[i]);
                }
                if(minQueue.Count - maxQueue.Count >= 2)
                {
                    median = minQueue.Dequeue();
                    maxQueue.Enqueue(median, median);
                }
                else if (minQueue.Count - maxQueue.Count <= -2)
                {
                    median = maxQueue.Dequeue();
                    minQueue.Enqueue(median, median);
                }
            }
            // 개수가 짝수일 경우 두 수의 평균을 취한다
            if(minQueue.Count == maxQueue.Count)
            {
                result = (minQueue.Dequeue() + maxQueue.Dequeue()) / 2;
            }
            else
            {
                result = median;
            }
            return result;
        }
    }
}