namespace _4._24homework
{

        internal class PriorityQueue<TElement>              // TElement 자료형
        {
            public struct Node                  // 구조체Node를 설정하고 안에 값(Telement)과 우선순위(Num)를 필요로하게 설정
            {
                public TElement element;
                public int Num;
            }
            private List<Node> nodes;           //      list 자료형 Node로 설정  그럼 인덱스안에 Node라는 단위로 element랑 Num들어감

            public PriorityQueue()                  // PriorityQueue안에 nodes를 설정
            {
                this.nodes = new List<Node>();
            }

            public int Count { get { return nodes.Count; } }                // list배열의 nodes의Count 를 확인할수 있는 함수 설정
            public void EnQueue(TElement element, int Num)
            {
                //일단 맨뒤에 추가
                Node newnode = new Node() { element = element, Num = Num };
                nodes.Add(newnode);                                         // nodes.맨뒤에 값과 우선순위를 가진 newnode를 추가
                int newnodeindex = nodes.Count - 1;                         // newnode의 위치확인 

                //맨뒤에 새로추가한 자식의 우선순위랑 부모의 우선순위비교후 바꾸기
                while (newnodeindex > 0)
                {
                    int parentindex = GetParentIndex(newnodeindex);             //새로넣은 자식의 배열의 위치로부터 부모의 배열위치 확인
                    Node parentNode = nodes[parentindex];                       // nodes List안에 부모의 배열안에 부모의 노드를 넣는다.

                    if (newnode.Num < parentNode.Num)                          //만일 자식의 우선순위가 부모의 우선순위보다 높으면
                    {
                        nodes[parentindex] = newnode;                           // parent배열위치에 newnode를 넣고
                        nodes[newnodeindex] = parentNode;                       // newnode배열위치에 parentNode 넣고
                        newnodeindex = parentindex;                             // 배열위치를 바꾼다. 배열위치를 안바꾸면 나중에 newnodeindex를 불러올때마다 parentNode값이 나온다. 
                    }
                    else break;
                }
            }
            //DeQueue-맨처음출력하고 삭제
            public TElement DeQueue()
            {
                Node Firstnode = nodes[0];                                         // 배열에서 제일 우선순위가 높은 값을 Fistnode로 설정
                Node Lastnode = nodes[nodes.Count - 1];                         // nodes의 마지막배열의 노드값을 Lastnode로 설정
                nodes[0] = Lastnode;                                            // 맨뒤에있는 lastnode를 맨앞으로 이동
                int Lastnodeindex = 0;                                          // lastnode의 위치 설정
                nodes.RemoveAt(nodes.Count - 1);                               // 맨뒤에있던 lastnode삭제

                while (Lastnodeindex < nodes.Count - 1)                                   //일단 Lastnode의 위치가 배열전부다 훓었을때까지 진행
                {       //Lastnodeindex가 우선순위가 제일높을때 그아래 자식들의 우선순위비교 
                    int LeftChildIndex = GetLeftChildIndex(Lastnodeindex);
                    int RightChildIndex = GetRightChildIndex(Lastnodeindex);
                    //자식이 둘다있을때 우선순위높은값을 lessNum 로 설정
                    int lessNum = nodes[LeftChildIndex].Num < nodes[RightChildIndex].Num ? nodes[RightChildIndex].Num : nodes[LeftChildIndex].Num;
                    int lessindex = nodes[LeftChildIndex].Num < nodes[RightChildIndex].Num ? RightChildIndex : LeftChildIndex;

                    // 만일 자식의 우선순위가 높으면
                    if (lessNum < Lastnode.Num)
                    {
                        nodes[Lastnodeindex] = nodes[lessindex];                             //자식인덱스에 부모값을 복사하고
                        nodes[lessindex] = Lastnode;                                        // 부모값은 자식인덱스에 넣고
                        Lastnodeindex = lessindex;                                          // 부모랑 자식의 인덱스값을 바꾼다.

                    }
                    //자식이 하나일때
                    else if (nodes[LeftChildIndex].Num < nodes[Lastnodeindex].Num)                   // 자식이 두명일때랑 동일하게 진행
                    {
                        nodes[Lastnodeindex] = nodes[LeftChildIndex];
                        nodes[LeftChildIndex] = Lastnode;
                        Lastnodeindex = LeftChildIndex;
                    }
                    //자식이 없으면 멈추게설정
                    else break;                                                                         // 주의 배열안에 node값을 바꾸더라도 index값도 바꿔줘야한다.
                }

                return Firstnode.element;                       // 가장 우선순위 높았던값을 출력
            }



            public int GetParentIndex(int Childintdex)    // EnQueue 구할때 부모인덱스를 그냥 EnQueue함수안에서 구현해도되지만 다른함수에도 사용하기 위해 구하는 공식을 따로빼놈        
            {
                return (Childintdex - 1) / 2;
            }
            public int GetLeftChildIndex(int parentIndex) // EnQueue 구할때 부모인덱스를 그냥 EnQueue함수안에서 구현해도되지만 다른함수에도 사용하기 위해 구하는 공식을 따로빼놈  
            {
                return parentIndex * 2 + 1;
            }
            public int GetRightChildIndex(int parentIndex) // EnQueue 구할때 부모인덱스를 그냥 EnQueue함수안에서 구현해도되지만 다른함수에도 사용하기 위해 구하는 공식을 따로빼놈 
            {
                return parentIndex * 2 + 2;
            }


        }

    }

    /*1. 우선순위큐 구현
     * --------------------------------------
     * 2. 힙정렬 기술면접 준비(힙,추가,삭제,완전이진트리 배열표현)
     * -----------------------------------------
     * 0. 응급실 만들기(우선순위큐로 골든타임을 입력받아서)                     
     * 급한 환자부터 치료하는 응급실
     * 0. 중간값 구하기(최대값,최소값)
     * (ex 숫자가 10000개 있을때 숫자중 정렬시켰을때 중간에 있는값 찾기== 예를들어 5000번째 큰 수 구하기
     * 
     * 
    */
    //EnQueue-맨뒤에 추가하는
    //DeQueue-맨처음출력하고 삭제


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }