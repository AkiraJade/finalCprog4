import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class pAdvLevel1 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class pAdvLevel1 extends World
{

    /**
     * Constructor for objects of class pAdvLevel1.
     * 
     */
      enum PlatformState {
        WAITING,      // Platform hasn't started moving yet
        MOVING,       // Platform is moving up
        STOPPED,      // Platform stopped at target position
        REMOVED,       // Platform has been removed from world
        DISAPPEAR,
        SPAWNED,
        APPEAR
    }
    
    enum SpikeState {
        NOT_CREATED,  // Spike doesn't exist yet
        CREATED,      // Spike exists but not ready to move
        READY,        // Player touched ground, spike ready to move
        MOVING,       // Spike is moving right
        STOPPED,       // Spike finished moving
        DISAPPEAR,
        APPEAR
    }
    
    int playerX;
    int playerY;
    Platform p2Up;
    Platform p5Dis;
    Platform p14Dis;
    Platform p15Dis;
    PlatformTrap pt2;
    Platform p18Up;
     PlatformTrap pt3;
     PlatformTrap pt4;
     Spike sp1;
     Spike sp2;
     Spike sp3;
     Spike sp4;
     Spike sp5;
     Spike sp7;
     Spike sp8;
    private int spikeCount = 0; 
    private int spikeX = 210;
    private int spawnDelay = 2;  // 60 acts ≈ 1 second
    private int spawnTimer = 0;
     SpikeState sp1State = SpikeState.READY;
     SpikeState sp2State = SpikeState.READY;
     SpikeState sp3State = SpikeState.READY;
     SpikeState sp4State = SpikeState.READY;
     SpikeState sp5State = SpikeState.READY;
     PlatformState p2UpState = PlatformState.WAITING;
     PlatformState p15DisState = PlatformState.WAITING;
     PlatformState p18UpState = PlatformState.WAITING;
    public pAdvLevel1()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1); 
        prepare();  
    }
    
    public void prepare(){
        Person  player1 = new Person();
        addObject(player1, 30, 180);
        Platform p1 = new Platform(80,200);
        addObject(p1, 40, 300);
       
        Platform p3 = new Platform(150, 100);
        addObject(p3, 195, 380);
        Platform p4 = new Platform(90, 50);
        addObject(p4, 310, 390);
        p5Dis = new Platform (90, 40);
        addObject(p5Dis, 310, 350); //disappearing below
        Platform p6 = new Platform(150, 100);
        addObject(p6, 430, 380);
        Platform p7 =  new Platform(60, 200);
        addObject(p7, 530, 300);
        Platform p8 = new Platform (330, 80);
        addObject(p8, 285, 240);
        Platform p9 = new Platform(40,200);
        addObject(p9, 620, 300);
        PlatformTrap pt1 = new PlatformTrap();
        GreenfootImage img = pt1.getImage();
        img.scale(40,200);
        addObject(pt1, 660, 300);
        Platform p10 = new Platform(80, 200);
        addObject(p10, 720, 300);
        Platform p11 = new Platform(400, 10);
        addObject(p11, 360, 140);
        Platform p12 = new Platform(330, 10);
        addObject(p12, 395, 90);
        Platform p13 = new Platform(30, 10);
        addObject(p13, 175, 90);
         p14Dis = new Platform(40, 10); //thin disappear
        addObject(p14Dis, 210, 90);
        p15Dis = new Platform(10, 50);
        addObject(p15Dis, 125, 305);
        pt2 = new PlatformTrap();
        GreenfootImage img2 = pt2.getImage();
        img2.scale(40, 100);//disappear pt2 alt for p2up
        addObject(pt2, 100, 380);
        Platform p16 = new Platform(250, 80);
        addObject(p16, 90, 0);
        Platform p17 = new Platform(600, 80);
        addObject(p17, 560, 0);
        p18Up = new Platform(25,50);
        addObject(p18Up, 547, 120);
        sp7 = new Spike();
        sp7.setRotation(90);
        sp8 = new Spike();
        sp8.setRotation(270);
        Platform p19 = new Platform(30,10);
        addObject(p19, 580, 170);
        Platform p20 = new Platform(10, 50);
        addObject(p20, 165, 110);
         pt3 = new PlatformTrap();
        GreenfootImage pt3Img = pt3.getImage();
        pt3Img.scale(390, 50);
        addObject(pt3, 365, 110);
         pt4 = new PlatformTrap();
        GreenfootImage pt4Img = pt4.getImage();
        pt4Img.scale(390, 150);
        addObject(pt4, 320, 275);
       //66 221
       pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act(){
        updatePlayerPosition();
        trapsV1();
        trapsV2();
    }
    
    public void updatePlayerPosition(){
        java.util.List<Person> players = getObjects(Person.class);
        
        if(players.isEmpty()){
            return;
        }
        
        Person player = players.get(0);
        playerX = player.getX();
        playerY = player.getY();
    }
    
    boolean spikeAppear = false;
    int frame = 0;
    int targetFrame = 120;
    boolean goesDown = false;
    int frame2 = 0;
    public void trapsV1(){
        
      
        //first spike
        if(playerX >= 80 && playerY <= 140 && sp1State == SpikeState.READY){
              sp1 = new Spike();
            addObject(sp1, 135, 192);
            sp1State = SpikeState.APPEAR;
        } else if(sp1State == SpikeState.APPEAR && playerX <=80){
            removeObject(sp1);
        } else if(playerX >= 200 && sp1State == SpikeState.APPEAR
        && sp2State != SpikeState.APPEAR && playerY == 181 && playerX <= 575){
            sp2 = new Spike();
            addObject(sp2, 180, 192);
            sp2State = SpikeState.APPEAR;
            
            
        } else if(sp2State == SpikeState.APPEAR  && playerY == 181 
        && playerX <= 575){
            
               if(spikeCount < 38 && playerX >= 240) {
            spawnTimer++;
            if (spawnTimer >= spawnDelay) {  // Spawns every 2 acts
                addObject(new Spike(), spikeX, 192);
                spikeX += 6;
                spawnTimer = 0;
                spikeCount++;
                  
            }
            
           
        }  
    } 
   if (sp2State == SpikeState.APPEAR && playerX >= 125 &&
         playerY == 311 ) {

    if (frame < targetFrame) {
        frame++;

        if (frame == targetFrame) {
            sp3 = new Spike();
            addObject(sp3, 492, 310);
            sp3.setRotation(270);
           
            sp3State = SpikeState.APPEAR;
        }
    }
}

if(sp3State == SpikeState.APPEAR && sp3 != null){
     sp3.setLocation(sp3.getX() - 10, sp3.getY());
     
     if(sp3.getX() <=50){
         removeObject(sp3);
         sp3 = null;
     }
}
          if (playerY >= 310 && playerY <= 315){
            goesDown = true;
            removeObject(pt4);
        }
        //platform disappear
        if (goesDown && playerX >= 310 && playerX <= 320)
{
    removeObject(p5Dis);
    p5Dis = null;
}

if (p5Dis == null && sp4State == SpikeState.READY && playerY == 346){
    if(frame2 < targetFrame){
        frame2++;
        if(frame2 == targetFrame){
            sp4 = new Spike();
            GreenfootImage sp4Img = sp4.getImage();
            sp4Img.scale(50, 15);
            addObject(sp4, 312, 357); 
            sp4State = SpikeState.APPEAR;
        }
    }
}

if (playerX <= 170 && p5Dis == null &&
    p15DisState == PlatformState.WAITING &&
    p2UpState == PlatformState.WAITING) {

    removeObject(p15Dis);
    p2Up = new Platform(40, 130);
    addObject(p2Up, 100, 395);
    p2UpState = PlatformState.APPEAR;
    p15DisState = PlatformState.DISAPPEAR;
}

if (playerX <= 105 &&
    p15DisState == PlatformState.DISAPPEAR &&
    p2UpState == PlatformState.APPEAR) {

     p15Dis = new Platform(10, 50);
        addObject(p15Dis, 125, 305);

    p15DisState = PlatformState.APPEAR;
}

if(p2UpState == PlatformState.APPEAR && p15DisState == PlatformState.APPEAR){
    p2UpState = PlatformState.MOVING;
}

if(p2UpState == PlatformState.MOVING){
    p2Up.setLocation (p2Up.getX(), p2Up.getY() - 5);
    
    if(p2Up.getY() <= 265){
        p2UpState = PlatformState.STOPPED;
    }
}

        
    }
    
    boolean jump = false;
    boolean spikeChain = false;
    int spikeCount2 = 0;
    int spawnTimer2 = 0;
    int spikeX2 = 520;
    int frame3 = 0;
    public void trapsV2(){
        if(playerY == 66 && playerX >= 221 && 
        sp5State == SpikeState.READY){
            sp5 = new Spike();
        addObject(sp5, 175, 77);
        sp5State = SpikeState.APPEAR;
        
        }
        
        if(sp5State == SpikeState.APPEAR 
        && playerX >= 412 && p18UpState == PlatformState.WAITING){
            p18UpState = PlatformState.MOVING;
        }
        
        if(p18UpState == PlatformState.MOVING && playerX >= 412){
            p18Up.setLocation(p18Up.getX(), p18Up.getY() - 10);
            
            if(p18Up.getY() <= 62){
                p18UpState = PlatformState.STOPPED;
            }
        }
        
        if(p18UpState == PlatformState.STOPPED){
            p18Up.setLocation(p18Up.getX() - 7, p18Up.getY());
            
            if(p18Up.getX() <= 202){
                p18Up.setLocation(p18Up.getX(), p18Up.getY() - 10);
                
                
            }
            
            if(p18Up.getY() <= 0){
                p18UpState = PlatformState.DISAPPEAR;
            }
            //205, 19
        }
        
        if(playerX >= 205 && playerY <= 19 & !jump){
            jump = true;
        }
        
        if(playerX >= 408 && jump){
            spikeChain = true;
        }
        
        if(jump && spikeChain){
             if(spikeCount2 < 41) {
            spawnTimer2++;
            if (spawnTimer2 >= spawnDelay) {  // Spawns every 2 acts
                Spike sp6 = new Spike();
                sp6.setRotation(180);
                addObject(sp6, spikeX2, 47);
                spikeX2 -= 6;
                spawnTimer2 = 0;
                spikeCount2++;
                
            }
                 
        }
        
        if(spikeChain && playerY <= 15){
            addObject(sp7, 222, 20);
             addObject(sp8, 252, 20);
        }
        
        if(spikeChain && playerX >= 195 && playerX <= 250){
            
            if(frame3 < targetFrame){
                frame3++;
                
                if(frame3 == targetFrame){
                    removeObject(p14Dis);
                    p14Dis = null;
                }
                
            }
        }
        
        if (p14Dis == null && playerY >= 115){
            removeObject(pt3);
        }
        //195, 230
    
    }
}
}