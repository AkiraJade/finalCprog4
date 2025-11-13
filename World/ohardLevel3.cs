import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class ohardLevel3 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class ohardLevel3 extends World
{

    /**
     * Constructor for objects of class ohardLevel3.
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
    Platform p2;
    int playerX;
    int playerY;
    int frameCount = 0;
    int frameLimit = 120;
   
    PlatformState p2State = PlatformState.WAITING;
    Platform p3;
    Platform p5Up;
    Spike sp1;
    Spike sp3;
    SpikeState sp1State = SpikeState.CREATED;
    PlatformState p5UpState = PlatformState.WAITING;
    PlatformState p11PushState = PlatformState.WAITING;
    Platform p11Push;
    public ohardLevel3()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
         super(750, 400, 1); 
         prepare();
    }
    
    public void prepare(){
          Person player1 = new Person();
        addObject(player1, 10, 180);
         Platform p1 = new Platform(100,200);
        addObject(p1, 50, 300);
        p2 = new Platform(100, 20);
        addObject(p2, 200, 210);
        PlatformTrap pt1 = new PlatformTrap();
        addObject(pt1, 350, 210);
        GreenfootImage img = pt1.getImage();
        img.scale(100, 20);
        
        Platform p4 = new Platform(100, 20);
        addObject(p4, 420, 330);
        
          
        p5Up = new Platform(30,150);
        addObject(p5Up, 455, 399);
        Platform p6 = new Platform(100, 20);
        addObject(p6, 420, 399);
        PlatformTrap pt2 = new PlatformTrap();
        GreenfootImage img2 = pt2.getImage();
        img2.scale(100, 100);
        addObject(pt2, 420, 380);
        Platform p8 = new Platform(100, 100);
        addObject(p8, 570, 370);
        Platform p9 = new Platform(100, 20);
        addObject(p9, 570, 270);
        Platform p10 = new Platform(150, 50);
        addObject(p10, 695, 380);
        p11Push = new Platform(25,50);
        addObject(p11Push, 610, 345);
        
        hardPortal hPor = new hardPortal(3);
        addObject(hPor, 739, 320);
        pauseBtn pauseButton = new pauseBtn();
        addObject(pauseButton, 700, 30);
    }
    
    public void act(){
        updatePlayerPosition();
        trapsV1();
        trapsV2();
        trapsV3();
        
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
    
    boolean didJump = false;
    public void trapsV1(){
        
        //275, 140
        if(playerX >= 260 && playerY <= 180 ){
            didJump = true;
            
        } 
        
        if(playerX >= 149 && 
        p2State == PlatformState.WAITING && !didJump){
            p2State = PlatformState.DISAPPEAR;
            
        } else if(p2State == PlatformState.DISAPPEAR){
            
            if(frameCount <= frameLimit && !didJump){
                frameCount++;
                
                if(frameCount == frameLimit){
                    removeObject(p2);
                }
                
            }
        } 
        if(didJump && playerY >= 280  && playerX < 295){
             p3 = new Platform(50, 10);
             addObject(p3, 280, 330);
        }
    }
    
    boolean goUp = false;
     boolean readyToMove = false;
     //430 370
    public void trapsV2(){
        
        //455 240
        if (playerX >= 420 && sp1State == SpikeState.CREATED){
            sp1State = SpikeState.READY;
        } else if (sp1State == SpikeState.READY){
            sp1 = new Spike();
        addObject(sp1,455, 312);
          sp1State = SpikeState.APPEAR;
       
       
        }
        
        if(!readyToMove && 
        sp1State == SpikeState.APPEAR && 
        playerX <= 420 && playerY >= 300){
            readyToMove = true;
        }
        
        
        
        if(playerX >= 420 && playerY <= 240 && 
        sp1State == SpikeState.APPEAR && 
        p5UpState == PlatformState.WAITING && readyToMove) {
            sp1State = SpikeState.MOVING;
            p5UpState = PlatformState.MOVING;
        } else if(sp1State == SpikeState.MOVING 
        && p5UpState == PlatformState.MOVING && !goUp){
            sp1.setLocation(sp1.getX() - 5, sp1.getY() );
            p5Up.setLocation(p5Up.getX(), p5Up.getY() - 20);
            //247, 305
            if(sp1.getX() <= 420 && p5Up.getY() <= 300){
                 sp1State = SpikeState.STOPPED;
            p5UpState = PlatformState.STOPPED;
            Spike sp2 = new Spike();
            addObject(sp2,455,176);
            }
    
        }
    }
    
    //534, 312
    boolean spawnOne = false;
    private int spikeCount = 0; 
    private int spikeX = 537;
    private int spawnDelay = 10;  // 60 acts ≈ 1 second
    private int spawnTimer = 0;
    
    
    public void trapsV3(){
        if (playerX >= 430 && playerY >= 370 && !spawnOne){
               Platform p7 = new Platform(100, 20);
               addObject(p7, 470, 399);
               spawnOne = true;
        } else if(spawnOne){
            if (spikeCount < 3 && playerX >= 550) {
            spawnTimer++;
            if (spawnTimer >= spawnDelay) {  // Spawns every 2 acts
                addObject(new Spike(), spikeX, 312);
                spikeX += 30;
                spawnTimer = 0;
                spikeCount++;
            }
        }
        
        }
        
        if(playerX >= 660 && p11PushState == PlatformState.WAITING) {
            p11PushState = PlatformState.MOVING;
        } else if(p11PushState == PlatformState.MOVING){
             p11Push.setLocation(p11Push.getX() + 20, p11Push.getY());
             if(playerX >= 660){
                 sp3 = new Spike();
                 addObject(sp3, 700, 347);
             }
             if(p11Push.getX() >= 749){
                 p11PushState = PlatformState.STOPPED;
                  removeObject(p11Push);
             }
        } 
        
        
    }
    
}