import greenfoot.*;  // (World, Actor, GreenfootImage, Greenfoot and MouseInfo)

/**
 * Write a description of class pAdvLevel3 here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class pAdvLevel3 extends World
{

    /**
     * Constructor for objects of class pAdvLevel3.
     * 
     */
     enum PlatformState {
        WAITING, MOVING, STOPPED, REMOVED, DISAPPEAR, SPAWNED, APPEAR
    }

    enum SpikeState {
        NOT_CREATED, CREATED, READY, MOVING, STOPPED, DISAPPEAR, APPEAR
    }

    
    int playerX;
    int playerY;
    Spike sp1;
    Spike sp2;
    Spike sp3;
    Spike sp4;
    Platform p6Dis;
    Platform rotating;
    Platform p9Up;
    PlatformState rotatingState = PlatformState.WAITING;
    SpikeState sp1State = SpikeState.CREATED;
    SpikeState sp2State = SpikeState.CREATED;
    SpikeState sp3State = SpikeState.CREATED;
    SpikeState sp4State = SpikeState.CREATED;
    PlatformState p9UpState = PlatformState.WAITING;
    public pAdvLevel3()
    {    
        // Create a new world with 600x400 cells with a cell size of 1x1 pixels.
        super(750, 400, 1);
        prepare();
    }
    
    public void  prepare(){
         
        Platform p1 = new Platform(100, 150);
        addObject(p1, 30, 340);
        rotating = new Platform(100, 20);
        addObject(rotating, 130, 275);
        Platform p2 = new Platform (50, 150);
        addObject(p2, 250, 340);
        Platform p3 = new Platform (50, 150);
        addObject(p3, 350, 340);
        Platform p4 = new Platform (50, 150);
        addObject(p4, 450, 340);
        Platform p5 = new Platform (50, 150);
        addObject(p5, 540, 340);
        p6Dis = new Platform (50, 150);
        addObject(p6Dis, 580, 340);
        Platform p7 = new Platform(100, 150);
        addObject(p7, 730, 340);
        Platform p8 = new Platform(40, 150);
        addObject(p8, 620, 340);
         p9Up = new Platform(25, 150);
        addObject(p9Up, 612, 340);
        FakeAdvPortal fp1 = new FakeAdvPortal();
        addObject(fp1, 725, 230);
        
        
        Person player1 = new Person();
        addObject(player1, 30, 246);
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
    boolean jump = false;
    boolean secondJump = false;
    boolean spawnOne = false;
    boolean forward= false;
    Shuriken s1;
    boolean returned;
    public void trapsV1(){
        if (rotatingState == PlatformState.WAITING && playerX >= 95){
            rotatingState = PlatformState.MOVING;
        }
        
        if(playerX >= 65 && sp1State == SpikeState.CREATED){
            sp1 = new Spike();
        addObject(sp1, 90, 257);
        sp1State = SpikeState.APPEAR;
        }
        if(sp1State == SpikeState.APPEAR && playerX >= 100 && playerY <= 213 && !jump){
            jump = true;
        }
        if(sp1State == SpikeState.APPEAR && jump ){
            sp1.setLocation(sp1.getX() + 5, sp1.getY());
            
            if(sp1.getX() >= 130){
                sp1State = SpikeState.STOPPED;
            }
        }
        
        if(playerX >= 163 && sp1State == SpikeState.STOPPED && !spawnOne){
             Opponent op1 = new Opponent();
             addObject(op1, 30, 246);
              s1 = new Shuriken();
             addObject(s1, 50, 246);
             spawnOne = true;
             forward = true;
        }
        
        if(forward && !returned){
            s1.setLocation(s1.getX() + 2, s1.getY());
            
            if(s1.getX() >= 700){
                returned = true;
                forward = false;
            }
        }
        
        if(returned && !forward){
            s1.setLocation(s1.getX() - 2, s1.getY());
            
            if(s1.getX() <= 50){
                returned = false;
                forward = true;
            }
        }
        
        if(playerX >= 205 && playerY <= 210 && sp2State == SpikeState.CREATED){
            sp2 = new Spike();
        addObject(sp2, 250, 257);
        sp2State = SpikeState.APPEAR;
        }
        
        if(sp2State == SpikeState.APPEAR && playerX <= 180){
            removeObject(sp2);
            sp2State = SpikeState.DISAPPEAR;
        }
        
         if(playerX >= 300 && playerY <= 200 && sp3State == SpikeState.CREATED
         && sp2State == SpikeState.DISAPPEAR){
            sp3 = new Spike();
        addObject(sp3, 350, 257);
        sp3State = SpikeState.APPEAR;
        }
        
        if(sp3State == SpikeState.APPEAR && playerX <= 270){
            removeObject(sp3);
            sp3State = SpikeState.DISAPPEAR;
        }
        
       
    }
    
    boolean goDown = false;
    boolean oneTime = false;
    public void trapsV2(){
          if(playerX >= 395 && playerY <= 200 && sp4State == SpikeState.CREATED
         && sp3State == SpikeState.DISAPPEAR){
            sp4 = new Spike();
        addObject(sp4, 450, 257);
        sp4State = SpikeState.APPEAR;
        }
        
        if(sp4State == SpikeState.APPEAR && playerX <= 370){
            removeObject(sp4);
            sp4State = SpikeState.DISAPPEAR;
        }
        
        if(sp4State == SpikeState.DISAPPEAR && playerX >= 495 && playerY <= 195
        && !secondJump){
            secondJump = true;
        }
        
        if(secondJump && playerX <= 470 && !oneTime){
            sp4 = new Spike();
        addObject(sp4, 450, 257);
        }
        
        if(playerX >= 555){
            removeObject(p6Dis);
        }
        
        if(p9UpState == PlatformState.WAITING && playerX >= 572 
        && playerY <= 184){
            p9UpState = PlatformState.MOVING;
        }
        
        if(p9UpState == PlatformState.MOVING){
            p9Up.setLocation(p9Up.getX(), p9Up.getY() - 20);
            
            if(p9Up.getY() <= 210){
                p9UpState = PlatformState.STOPPED;
            }
        }
        
        if(!goDown && p9UpState == PlatformState.STOPPED && playerX <= 560){
            goDown = true;
        }
        
        if (goDown && p9UpState == PlatformState.STOPPED){
            p9Up.setLocation(p9Up.getX(), p9Up.getY() + 20);
             if(p9Up.getY()>= 390){
                p9UpState = PlatformState.DISAPPEAR;
            }
        }
        
    if(playerX >= 725 && playerY >= 240 && !oneTime){
        advPortal ap1 = new advPortal(5);
        addObject(ap1, 80, 231);
        oneTime = true;
        
    }
        
       //195
       //572, 184
    }
    
    
}