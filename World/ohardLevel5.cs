import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)
import java.util.ArrayList;
/**
 * Write a description of class ohardLevel here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class ohardLevel5 extends World
{

    /**
     * Constructor for objects of class ohardLevel.
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
    
    ArrayList<Spike> sp1DownSpikes = new ArrayList<Spike>();
    int playerX;
    int playerY;
    Platform p3Up;
    Platform p9Up;
    Spike sp1;
    Spike sp2;
    Spike sp3;
    Spike sp4;
    Platform p6;
    private int spikeCount = 0; 
    private int spikeX = 320;
    private int spawnDelay = 2;  
    private int spawnTimer = 0;
    SpikeState sp2State = SpikeState.READY;
    PlatformState p3UpState = PlatformState.WAITING;
       PlatformState p9UpState = PlatformState.WAITING;
       SpikeState sp4State = SpikeState.READY;
    Platform p5Left;
    PlatformState p5LeftState = PlatformState.WAITING;
    public ohardLevel5()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1); 
        prepare(); 
    }
    
    public void prepare(){
        Person  player1 = new Person();
        addObject(player1, 30, 280);
        Platform p1 = new Platform (150,100);
        addObject(p1, 70, 350);
        Platform p2 = new Platform (150, 100);
        addObject(p2, 260, 390);
        p3Up = new Platform(150, 50);
        addObject(p3Up, 260, 325);
        Platform p4 = new Platform(800, 100);
        addObject(p4, 360, 20);
        Platform p5 = new Platform(70, 100);
        addObject(p5, 420, 350);
        p6 = new Platform(60, 100);
        addObject(p6, 485, 350);
        Platform p7 = new Platform(80,100);
        addObject(p7, 550, 350);
        Platform p8 = new Platform (130, 100);
        addObject(p8, 700, 350);
        p9Up = new Platform(25, 150);
        addObject(p9Up, 397, 399);
        hardPortal hPor = new hardPortal(5);
        addObject(hPor, 704, 270);
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
    
    public void createSpikeRow(int startX, int y, int count, boolean pointDown) {
    for(int i = 0; i < count; i++) {
        sp1 = new Spike();
        
        
        if(pointDown) {
            sp1.getImage().rotate(180);
        }
        
        addObject(sp1, startX + (i * 30), y);
        sp1DownSpikes.add(sp1);
        }
    }
    public void trapsV1(){
        if (playerX >= 270 && p3UpState == PlatformState.WAITING){
            p3UpState = PlatformState.MOVING;
        } else if(p3UpState == PlatformState.MOVING){
            p3Up.setLocation(p3Up.getX(), p3Up.getY() - 10);
            if(p3Up.getY() <= 150){
                p3UpState = PlatformState.STOPPED;
                createSpikeRow(265, 77, 3, true);
               p5Left = new Platform(25,70);
               addObject(p5Left, 0, 100);
               p5LeftState = PlatformState.MOVING;
            }
        } 
    
        //for p5Left
        
        if(p5LeftState == PlatformState.MOVING){
            p5Left.setLocation(p5Left.getX() + 5, p5Left.getY());
            
            if(p5Left.getX() >= 260){
                p5LeftState = PlatformState.STOPPED;
            }
        }
        
    }
    
    
   boolean triggerLong = false;
    ArrayList<Spike> spawnedSpikes = new ArrayList<>();
   boolean backAgain = false;
    public void trapsV2(){
    
    if (playerX >=250 && !triggerLong  && p5LeftState == PlatformState.STOPPED){
        triggerLong = true;
    }
    if (spikeCount < 20 && triggerLong
    && p5LeftState == PlatformState.STOPPED ) {
        spawnTimer++;
        if (spawnTimer >= spawnDelay) {  
            Spike newSpike = new Spike();
            addObject(newSpike, spikeX, 332);
            spawnedSpikes.add(newSpike);  
            spikeX -= 6;
            spawnTimer = 0;
            spikeCount++;
        }
    } else if(spikeCount == 20 && playerX <= 146 && !backAgain){
        
    for (Spike spike : spawnedSpikes) {
        if (spike.getWorld() != null) {  
            removeObject(spike);
         
        } 
        
        if(spike.getWorld() == null){
            backAgain = true;
        }
    }
    spawnedSpikes.clear();  
    } 
    else if(backAgain && p5LeftState == PlatformState.STOPPED &&playerX >= 165
        && playerY <= 250 && sp2State == SpikeState.READY && triggerLong){
        sp2 = new Spike();
        addObject(sp2, 200, 332);
        sp2State = SpikeState.APPEAR;
        } else if(sp2State == SpikeState.APPEAR && playerX <= 145 
        && playerY == 281){
            removeObject(sp2);
            sp2State = SpikeState.DISAPPEAR;
        } 
        
        
    }
    
    public void trapsV3(){
        //360 210
         if(p9UpState == PlatformState.WAITING && playerX >= 350
         && playerY <= 280){
            p9UpState = PlatformState.MOVING;
         } else if(p9UpState == PlatformState.MOVING){
             p9Up.setLocation(p9Up.getX(), p9Up.getY() - 20);
             if (p9Up.getY() <= 250){
                 p9UpState = PlatformState.STOPPED;
             }
         } else if(p9UpState == PlatformState.STOPPED){
               p9Up.setLocation(p9Up.getX(), p9Up.getY() + 20);
             if (p9Up.getY() >= 399){
                 p9UpState = PlatformState.DISAPPEAR;
             }
         } else if(p9UpState == PlatformState.DISAPPEAR 
         && playerX >= 350
         && playerY <= 270
         && sp4State == SpikeState.READY){
              sp4 = new Spike();
        addObject(sp4, 402, 292);
        sp4State = SpikeState.APPEAR;
         } else if(sp4State == SpikeState.APPEAR && playerX <= 336
         && playerY >= 321){
             removeObject(sp4);
             sp4State = SpikeState.DISAPPEAR;
         } else if(sp4State == SpikeState.DISAPPEAR && playerX >= 440){
             removeObject(p6);
         }
         
       
    }
    
    
}
